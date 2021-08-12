using AutoMapper;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Data.Repository;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;
using System.Collections.Generic;
using System.Linq;

namespace Life_Healthy_API.Business
{
    public class FoodBL
    {
        private readonly IMapper _mapper;
        private readonly FoodRepository _foodRepository;
        private readonly UsuarioRepository _userRepository;

        public FoodBL(IMapper mapper, FoodRepository foodRepository, UsuarioRepository userRepository)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _userRepository = userRepository;
        }

        public int InsertFood_BL(FoodRequest food)
        {
            var idUser = _userRepository.GetUsuario(food.UserId);

            if (idUser.UsuarioId == 1)
                return 0;

            var foodEntity = _mapper.Map<AlimentoEntity>(food);
            var insertFood = _foodRepository.InsertFood_DAO(foodEntity);
            return insertFood;
        }


        public FoodResponse GetFoodById_BL(int id)
        {
            var foodEntity = _foodRepository.GetFoodById_DAO(id);
            var foodResponse = _mapper.Map<FoodResponse>(foodEntity);

            return foodResponse;
        }

        public IEnumerable<FoodResponse> GetFoodByUserId_BL(int id)
        {
            var foodEntity = _foodRepository.GetFoodByUserId_DAO(id);
            var foodResponse = foodEntity.Select(x => _mapper.Map<FoodResponse>(x));

            return foodResponse;
        }

        public int DeleteFood_BL(int id)
        {
            var foodEntity = _foodRepository.DeleteFood_DAO(id);

            return foodEntity;
        }

        public Response UpdateFood_BL(FoodUpdateRequest food)
        {
            var weightID = _foodRepository.GetFoodById_DAO(food.UserId);
            var idUser = _userRepository.GetUsuario(food.UserId);

            if (weightID == null || idUser == null)
            {
                return new Response { Message = (weightID == null ? "Alimento não encontrado!" : "Usuario não encontrado!") };
            }

            var foodEntity = _mapper.Map<AlimentoEntity>(food);
            var response =_foodRepository.UpdateFood_DAO(foodEntity);

            return new Response { Message = response.ToString() };

            //if (weightID == null || idUser == null)
            //{
            //    return new Response { Message = (weightID == null ? "Alimento não encontrado!" : "Usuario não encontrado!")};
            //}
            //else
            //{
            //    var foodEntity = _mapper.Map<AlimentoEntity>(food);
            //    _foodRepository.UpdateFood_DAO(foodEntity);

            //    return new Response { Message = "Alimento atualizado sucesso!"};
            //}
        }
    }
}
