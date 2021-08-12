using AutoMapper;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;

namespace Life_Healthy_API.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioRequest, UsuarioEntity>().ReverseMap();
            CreateMap<UserLoginResponse, UsuarioEntity>().ReverseMap();
            CreateMap<UsuarioEntity, UserLoginResponse>().ReverseMap();

            CreateMap<UsuarioEntity, UsuarioResponse>().ReverseMap();
            CreateMap<UsuarioResponse, UsuarioEntity>().ReverseMap();
        }
    }
}