using AutoMapper;
using YES.Domain.Auth;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.Batches;
using YES.Dtos.Branch;
using YES.Dtos.Course;
using YES.Dtos.ExamResult;
using YES.Dtos.Gallery;
using YES.Dtos.Home;
using YES.Dtos.News;
using YES.Dtos.Result;
using YES.Dtos.Student;
using YES.Dtos.Syllabus;

namespace YES.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(pts => pts.BranchDto, opt => opt.MapFrom(ps => ps.Branch))
                .ForMember(pts => pts.EducationalDetailDtos, opt => opt.MapFrom(ps => ps.EducationalDetails))
                .ForMember(pts => pts.ResultCertificateDto, opt => opt.MapFrom(ps => ps.ResultCertificate))
                .ForMember(pts=>pts.ExamResultDtos,opt=>opt.MapFrom(ps=>ps.ExamResults))
                .ForMember(pts => pts.RegistrationDto, opt => opt.MapFrom(ps => ps.Registration)).ReverseMap();
            CreateMap<Exam, ExamDto>()
                .ForMember(pts => pts.course, opt => opt.MapFrom(ps => ps.Course)).ReverseMap();
            CreateMap<Course, CourseDto>().ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Course, CourseEnquiryDto>()
                .ForMember(pts => pts.BatchDto, opt => opt.MapFrom(ps => ps.BatchDetails))
                .ForMember(pts => pts.SyllabusDtos, opt => opt.MapFrom(ps => ps.SyllabusDetails)).ReverseMap();
            CreateMap<ContactUs, ContactUsDto>().ReverseMap();
            CreateMap<Registration, RegistrationDto>().ReverseMap();
            CreateMap<EducationalDetail, EducationalDetailDto>().ReverseMap();
            CreateMap<ExamResult, ExamResultDto>()
            .ForMember(pts => pts.ExamDto, opt => opt.MapFrom(ps => ps.Exams)).ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Branch, BranchDto>()
                .ForMember(pts => pts.userDto, opt => opt.MapFrom(ps => ps.User)).ReverseMap();
            CreateMap<ResultCertificate, ResultCertificateDto>().ReverseMap();
            CreateMap<SyllabusDetail, SyllabusDto>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.Value.ToString("dd-MMMM-yyyy hh:mm tt")))
                .ForMember(pts => pts.course, opt => opt.MapFrom(ps => ps.Course)).ReverseMap();
            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<ImageGallery, GalleryDto>().ReverseMap();
            CreateMap<BatchDetails, BatchDto>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.Value.ToString("dd-MMMM-yyyy hh:mm tt")))
                .ForMember(pts => pts.courseDto, opt => opt.MapFrom(ps => ps.Course)).ReverseMap();
            CreateMap<CourseEnquiry, CourseEnquiryDto>().ReverseMap();
            //CreateMap<Branch,Bra>
        }

    }
}
