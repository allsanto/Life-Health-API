using AutoMapper;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;

namespace Life_Healthy_API.Profiles
{
    public class WeightProfile : Profile
    {
        public WeightProfile()
        {
            CreateMap<PesoEntity, WeightResponse>().ReverseMap()
                .ForMember(dest => dest.PesoId, opt => opt.MapFrom(src => src.WeightId))
                .ForMember(dest => dest.DataPeso, opt => opt.MapFrom(src => src.DateWeigth))
                .ForMember(dest => dest.Peso, opt => opt.MapFrom(src => src.Weigth))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<PesoEntity, WeightRequest>().ReverseMap()
                .ForMember(dest => dest.DataPeso, opt => opt.MapFrom(src => src.DateWeigth))
                .ForMember(dest => dest.Peso, opt => opt.MapFrom(src => src.Weigth))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<PesoEntity, WeightUpdateRequest>().ReverseMap()
               .ForMember(dest => dest.PesoId, opt => opt.MapFrom(src => src.PesoId))
               .ForMember(dest => dest.DataPeso, opt => opt.MapFrom(src => src.DateWeigth))
               .ForMember(dest => dest.Peso, opt => opt.MapFrom(src => src.Weigth))
               .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<WeightResponse, PesoEntity>().ReverseMap()
                .ForMember(dest => dest.WeightId, opt => opt.MapFrom(src => src.PesoId))
                .ForMember(dest => dest.DateWeigth, opt => opt.MapFrom(src => src.DataPeso))
                .ForMember(dest => dest.Weigth, opt => opt.MapFrom(src => src.Peso))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UsuarioId));
        }
    }
}
