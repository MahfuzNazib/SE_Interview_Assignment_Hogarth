using AutoMapper;
using Hogarth.UserManagement.Application.DTOs.User;
using Hogarth.UserManagement.Domain.Entities;

namespace Hogarth.UserManagement.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
