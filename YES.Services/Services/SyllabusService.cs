using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.Syllabus;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class SyllabusService : ISyllabusService
    {
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly IMapper _mapper;
        public SyllabusService(ISyllabusRepository syllabusRepository, IMapper mapper)
        {
            _syllabusRepository = syllabusRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<SyllabusDto>> CreateAsync(SyllabusDto entity)
        {
            APIResponse<SyllabusDto> response = new APIResponse<SyllabusDto>();
            var Model = _mapper.Map<SyllabusDetail>(entity);
            var result = await _syllabusRepository.Create(Model);

            if (result.Data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<SyllabusDto>(result.Data);
                response.Message = CommonResource.AddSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.OK;
            }
            return response;
        }

        public async Task<APIResponse> DeleteAsync(SyllabusDto entity)
        {
            APIResponse response = new APIResponse();
            var Model = _mapper.Map<SyllabusDetail>(entity);
            var result = await _syllabusRepository.DeleteRecord(Model);

            if (result != 0)
            {
                response.Success = true;
                response.Message = CommonResource.FetchSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<SyllabusDto>> EditAsync(SyllabusDto entity)
        {
            APIResponse<SyllabusDto> response = new APIResponse<SyllabusDto>();
            var Model = _mapper.Map<SyllabusDetail>(entity);
            var result = await _syllabusRepository.Edit(Model);

            if (result != 0)
            {
                response.Success = true;
                response.Message = CommonResource.UpdateSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Data = entity;
                response.Message = CommonResource.UpdateFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<IEnumerable<SyllabusDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<SyllabusDto>> response = new APIResponse<IEnumerable<SyllabusDto>>();
            var result = await _syllabusRepository.GetAll();

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<IEnumerable<SyllabusDto>>(result);
                response.Message = CommonResource.FetchSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<SyllabusDto>> GetByIdAsync(int id)
        {
            APIResponse<SyllabusDto> response = new APIResponse<SyllabusDto>();
            var result = await _syllabusRepository.GetById(id);

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<SyllabusDto>(result);
                response.Message = CommonResource.FetchSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
