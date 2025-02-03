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

            CreateMap<CreateChallengeDto, Challenge>();
            CreateMap<UpdateChallengeDto, Challenge>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping from SubmissionDto to Submission.
            // Ignore properties that are set automatically or are navigation properties.
            CreateMap<SubmissionDto, Submission>()
                .ForMember(dest => dest.SubmissionId, opt => opt.Ignore())
                .ForMember(dest => dest.Challenge, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Team, opt => opt.Ignore());
        }
    }
}
