
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using YES.Domain.Auth;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Utility.Common;
using YES.Utility.Enum;
using YES.Utility.Enums;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public BranchRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext dbContext
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            ) : base(dbFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<APIResponse<Branch>> Create(Branch entity)
        {
            APIResponse<Branch> response = new APIResponse<Branch>();
            if (await _userManager.Users.AnyAsync(u => Equals(u.Email, entity.EmailId)))
            {
                response.Data = entity;
                response.Message = CommonResource.EmailExist;
                response.Status = HttpStatusCode.OK;
                response.Success = false;
                return await Task.FromResult(response);
            }

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == entity.PhoneNo))
            {
                response.Data = entity;
                response.Message = CommonResource.MobileExist;
                response.Status = HttpStatusCode.OK;
                response.Success = false;
                return await Task.FromResult(response);
            }

            var user = new ApplicationUser
            {
                Email = entity.EmailId,
                FirstName = entity.Name,
                LastName = entity.LastName,
                UserName = entity.EmailId,
                //Password = entity.PhoneNo,
                Password = "Password@123",
                PhoneNumber = entity.PhoneNo,
                LockoutEnabled = true,
            };
            user.SecurityStamp = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)
            {
                response.Data = entity;
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.OK;
                response.Success = false;
                return await Task.FromResult(response);
            }

            entity.User = user;
            string roleName = Roles.BranchAdmin.ToString();
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    response.Data = entity;
                    response.Message = "Failed to create role.";
                    response.Status = HttpStatusCode.OK;
                    response.Success = false;
                    return await Task.FromResult(response);
                }
            }
            var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addToRoleResult.Succeeded)
            {
                response.Data = entity;
                response.Message = "User created and role not assigned successfully.";
                response.Status = HttpStatusCode.OK;
                response.Success = false;
                return await Task.FromResult(response);
            }

            entity.UserId = user.Id;
            entity.BranchType = (int)BranchType.Other;
            Add(entity);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            response.Data = entity;
            response.Message = CommonResource.AddSuccess;
            response.Status = HttpStatusCode.OK;
            response.Success = true;
            return await Task.FromResult(response).ConfigureAwait(false);

        }

        public async Task<int> DeleteRecord(Branch entity)
        {
            var record = await First(FilterById(entity.Id));
            record.IsDeleted = !record.IsDeleted;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            return await List(FilterByIsDeleted()).ToListAsync();
        }

        public async Task<Branch> GetById(int id)
        {
            return await First(FilterById(id));
        }

        public async Task<int> Edit(Branch entity)
        {
            var record = await First(FilterById(entity.Id));
            record.Name = entity.Name;
            record.LastName = entity.LastName;
            record.Address = entity.Address;
            record.EmailId = entity.EmailId;
            record.PhoneNo = entity.PhoneNo;
            record.City = entity.City;
            record.State = entity.State;
            record.Qualification = entity.Qualification;
            record.Profile = entity.Profile;
            record.Signature = entity.Signature;
            record.UpdatedBy = entity.UpdatedBy;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
        public async Task<int> UpdateStatus(int Id)
        {
            var record = await First(FilterById(Id));
            record.IsVerify = !record.IsVerify;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
        public async Task<Branch> GetBranchDetails(int Id)
        {
            return await _dbContext.Branches.Include(x => x.User).Include(x => x.Student).FirstAsync(x => x.Id == Id);
        }
        Expression<Func<Branch, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }
        Expression<Func<Branch, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }


    }
}
