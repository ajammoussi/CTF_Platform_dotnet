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
            CreateMap<Challenge, ChallengeDto>();
            CreateMap<Submission, SubmissionDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<SupportTicket, TicketDto>();
        }
    }
}
