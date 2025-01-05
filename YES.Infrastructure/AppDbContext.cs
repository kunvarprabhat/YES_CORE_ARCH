using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YES.Domain.Auth;
using YES.Domain.Entities;
using YES.Infrastructure.Seeds;

namespace YES.Infrastructure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BatchDetails> BatchDetails { get; set; }
        public DbSet<CourseEnquiry> CourseEnquiries { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public DbSet<TopOffer> TopOffers { get; set; }
        public DbSet<SyllabusDetail> SyllabusDetails { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EducationalDetail> EducationalDetails { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<ResultCertificate> ResultCertificates { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            modelBuilder.Seed();
        }
    }
}
