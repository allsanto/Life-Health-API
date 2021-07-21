using AutoMapper;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;

namespace Life_Healthy_API.Profiles
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<FoodRequest, AlimentoEntity>().ReverseMap()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.NumCalories, opt => opt.MapFrom(src => src.NumCalorias))
                .ForMember(dest => dest.DateFood, opt => opt.MapFrom(src => src.DataAlimento))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UsuarioId));

            CreateMap<AlimentoEntity, FoodRequest>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumCalorias, opt => opt.MapFrom(src => src.NumCalories))
                .ForMember(dest => dest.DataAlimento, opt => opt.MapFrom(src => src.DateFood))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<AlimentoEntity, FoodResponse>().ReverseMap()
                .ForMember(dest => dest.AlimentoId, opt => opt.MapFrom(src => src.FoodId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumCalorias, opt => opt.MapFrom(src => src.NumCalories))
                .ForMember(dest => dest.DataAlimento, opt => opt.MapFrom(src => src.DateFood))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<AlimentoEntity, FoodUpdateRequest>().ReverseMap()
                .ForMember(dest => dest.AlimentoId, opt => opt.MapFrom(src => src.FoodId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumCalorias, opt => opt.MapFrom(src => src.NumCalories))
                .ForMember(dest => dest.DataAlimento, opt => opt.MapFrom(src => src.DateFood))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
