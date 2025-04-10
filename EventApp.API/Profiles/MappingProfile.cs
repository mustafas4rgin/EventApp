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
        CreateMap<EventuserRelDTO, EventUserRel>().ReverseMap();
        CreateMap<UpdateRoleDTO, Role>().ReverseMap();
        CreateMap<User, UserListDTO>().ReverseMap();
        CreateMap<Role, RoleListDTO>().ReverseMap();
        CreateMap<Event, EventDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserWithRoleDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        CreateMap<Event, EventsWithUsersDTO>()
            .ForMember(dest => dest.BookedUsers, opt => opt.MapFrom(src => src.BookedUsers));
        CreateMap<Event, GetEventsWithCreatedUserDTO>()
            .ForMember(dest => dest.CreatedByUser,opt => opt.MapFrom(src => src.CreatedByUser));
        CreateMap<User, UserWithEventsDTO>()
            .ForMember(dest => dest.BookedEvents,opt => opt.MapFrom(src => src.BookedEvents));
    }
}