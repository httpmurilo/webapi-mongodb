using AutoMapper;
using Source.Api.Dto;
using Source.Domain.Model;

namespace Source.Api.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News,NewsDto>();
        }
    }
}