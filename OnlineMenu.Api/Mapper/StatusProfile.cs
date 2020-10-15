using AutoMapper;
using OnlineMenu.Application.Dto;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Models;

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