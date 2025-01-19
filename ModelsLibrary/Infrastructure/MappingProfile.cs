using ModelsLibrary.Dto;
using ModelsLibrary.Models.Dto;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ModelsLibrary.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserPostResponseDto>();
            CreateMap<User, UserDto>();

            CreateMap<Reaction, ReactionDto>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User));

            CreateMap<Post, PostResponseDto>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User))
                .ForMember(d => d.Reactions, o => o.MapFrom(s => s.Reactions));
        }
    }
}
