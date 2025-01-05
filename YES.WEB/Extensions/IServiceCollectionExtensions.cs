using Microsoft.AspNetCore.Localization;
using System.Globalization;
using YES.Infrastructure;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Services.IServices;
using YES.Services.Services;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.Repositories;
using Yesscc.Services.IServices;
using YES.Comman.Interfaces;
using YES.Comman;
using YES.Services.ComunicationServices; 
using YES.Services.CommunicationServices;
using YES.Services.Configration;

namespace YES.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseMySql(configuration.GetConnectionString("YESConnection"));
            //    //options.UseLazyLoadingProxies();

            //});
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddTransient<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddTransient<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        public static IServiceCollection AddConfigration(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<MailSettings>(configuration?.GetSection("MailSettings")!);
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
             .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<ICourseRepository, CourseRepository>()
            .AddScoped<IContactUsRepository, ContactUsRepository>()
            .AddScoped<IGalleryRepository, GalleryRepository>()
            .AddScoped<INewsRepository, NewsRepository>()
            .AddScoped<IRegistrationRepository, RegistrationRepository>()
            .AddScoped<IStudentRepository, StudentRepository>()
            .AddScoped<IExamRepository, ExamRepository>()
            .AddScoped<IExamResultRepository, ExamResultRepository>()
            .AddScoped<IBranchRepository, BranchRepository>()
            .AddScoped<IResultCertificateRepository, ResultCertificateRepository>()
            .AddScoped<ISyllabusRepository, SyllabusRepository>()
            .AddScoped<IBatchesRepository, BatchesRepository>()
            .AddScoped<ICourseEnquiryRepository, CourseEnquiryRepository>()
            ;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<IContactUsService, ContactUsService>()
            .AddScoped<IGalleryService, GalleryService>()
            .AddScoped<INewsService, NewsService>()
            .AddScoped<IRegistrationService, RegistrationService>()
            .AddScoped<IStudentService, StudentService>()
            .AddScoped<IExamService, ExamService>()
            .AddScoped<IExamResultService, ExamResultService>()
            .AddScoped<IBranchService, BranchService>()
            .AddScoped<IResultCertificateService, ResultCertificateService>()
            .AddScoped<ISyllabusService, SyllabusService>()
            .AddScoped<IBatchesService, BatchesService>()
            .AddScoped<ICourseEnquiryService, CourseEnquiryService>()
            .AddTransient<ICommunication, Communication>()
            ;
        }
        public static IServiceCollection AddSessionWithOptions(this IServiceCollection services)
        {
            return services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
        }
        public static IServiceCollection AddLanguage(this IServiceCollection services)
        {
            return
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("hi-HI"),
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedUICultures = supportedCultures;
            });
        }
    }
}
