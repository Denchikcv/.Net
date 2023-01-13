using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CollegeService;



namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<College, CollegereadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<CollegePublishedDto, College>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcCollegeModel, College>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.CollegeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }

}