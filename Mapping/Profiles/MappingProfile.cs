using AutoMapper;
using CTF_Platform_dotnet.Models;
using CTF_Platform_dotnet.DTOs;


namespace CTF_Platform_dotnet.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Challenge, ChallengeDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty.ToString()));
            CreateMap<Submission, SubmissionDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<SupportTicket, TicketDto>();
        }
    }
}
