using AutoMapper;
using Source.Api.Dto;
using Source.Domain.Model;

namespace Source.Api.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author,AuthorDto>();
        }
    }
}