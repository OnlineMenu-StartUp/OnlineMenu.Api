using AutoMapper;
using OnlineMenu.Application.Dto;
using OnlineMenu.Domain;

namespace OnlineMenu.Api.Mapper
{
    public class StatusProfile: Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>();
            CreateMap<StatusDto, Status>();
        }
    }
}