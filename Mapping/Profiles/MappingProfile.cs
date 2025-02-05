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
            CreateMap<Submission, SubmissionAdminDto>();
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
