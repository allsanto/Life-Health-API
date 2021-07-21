using AutoMapper;
using Life_Healthy_API.Controllers;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Data.Repository;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Life_Healthy_API.Business
{
    public class WeightBL
    {
        private readonly IMapper _mapper;
        private readonly WeightRepository _weightRepository;
        private readonly UsuarioRepository _userRepository;

        public WeightBL(IMapper mapper, WeightRepository weightRepository, UsuarioRepository userRepository)
        {
            _mapper = mapper;
            _weightRepository = weightRepository;
            _userRepository = userRepository;
        }

        public int InsertWeight_BL(WeightRequest weightRequest)
        {
            var idUser = _userRepository.GetUsuario(weightRequest.UserId);

            if (idUser == null)
            {
                return 0;
            }

            var weightEntity = _mapper.Map<PesoEntity>(weightRequest);
            var insertWeight = _weightRepository.InsertWeight_DAO(weightEntity);
            return insertWeight;
        }


        public WeightResponse GetWeightById_BL(int id)
        {
            var weightEntity = _weightRepository.GetWeightById_DAO(id);
            var weightResponse = _mapper.Map<WeightResponse>(weightEntity);

            return weightResponse;
        }

        public IEnumerable<WeightResponse> GetWeightByUserId_BL(int id)
        {
            var weightEntity = _weightRepository.GetWeightByUserId_DAO(id);
            var weightResponse = weightEntity.Select(x => _mapper.Map<WeightResponse>(x));

            return weightResponse;
        }

        public int DeleteWheight_BL(int id)
        {
            var weightEntity = _weightRepository.DeleteWeight_DAO(id);

            return weightEntity;
        }

        public Response UpdateWeight_BL(WeightUpdateRequest weight)
        {
            var weightID = _weightRepository.GetWeightById_DAO(weight.PesoId.Value);
            var idUser = _userRepository.GetUsuario(weight.UserId);

            if (weightID == null || idUser == null)
            {
                return new Response { Message = (weightID == null ? "Peso não encontrado!" : "Usuario não encontrado!")};
            }
            else
            {
                _weightRepository.UpdateWeight_DAO(weight.DateWeigth, weight.Weigth, weight.UserId, weight.PesoId.Value);
                return new Response { Message = "Peso atualizado!"};
            }
        }
    }
}
