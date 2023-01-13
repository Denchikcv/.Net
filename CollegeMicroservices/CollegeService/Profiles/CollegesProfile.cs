using AutoMapper;
using CollegeService.DTOs;
using CollegeService.Models;

namespace CollegeService.Profiles
{
    public class CollegesProfile : Profile
    {
        public CollegesProfile()
        {
            // Source -> Target
            CreateMap<College, CollegeReadDto>();
            CreateMap<CollegeCreateDto, College>();
            CreateMap<CollegeReadDto, CollegePublishedDto>();
            CreateMap<College, GrpcCollegeModel>()
            .ForMember(dest => dest.CollegeId, opt => opt.MapFrom(src =>src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src =>src.Publisher));
        }
    }
}