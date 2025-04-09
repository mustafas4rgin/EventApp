using AutoMapper;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;

namespace EventApp.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<EventDTO, Event>().ReverseMap();
        CreateMap<RoleDTO, Role>().ReverseMap();
        CreateMap<UpdateRoleDTO, Role>().ReverseMap();
    }
}