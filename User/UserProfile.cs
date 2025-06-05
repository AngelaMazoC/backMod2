using AutoMapper;

namespace firstback.user
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserDTO, UserRoleDTO>();
            CreateMap<UserRoleDTO, User>();
            CreateMap<User, UserRegisterDTO>();

            CreateMap<User, UserRoleDTO>().ForMember(dest => dest.rol, opt => opt.MapFrom(src => src.Role != null ? src.Role.rol : null));
        }
    }
}