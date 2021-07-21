using Life_Healthy_API.Business;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Life_Healthy_API.Controllers
{
    [Route("apilife/food")]
    public class AlimentoController : Controller
    {
        private readonly FoodBL _foodBL;
        public AlimentoController(FoodBL food)
        {
            _foodBL = food;
        }

        /// <summary>
        /// Inserir Alimento
        /// </summary>
        /// <param name="weightRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult InsertFood([FromBody] FoodRequest foodRequest)
        {
            var response = _foodBL.InsertFood_BL(foodRequest);

            if (response != 0)
            {
                return Ok("Alimento inserido com Sucesso!");
            }
            else
            {
                return NotFound(new Response { Message = "Usuário não encontrado!" });
            }
        }

        /// <summary>
        /// Retorna o Alimento pelo alimento_id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getFoodById/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(FoodResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetFoodById(int id)
        {
            var foodResponse = _foodBL.GetFoodById_BL(id);

            if (foodResponse != null)
            {
                return Ok(foodResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum alimento encontrado." });
            }
        }

        [HttpGet]
        [Route("getgetFoodByUserId/{id}")]
        public IActionResult GetFoodByUserId(int id)
        {
            var foodResponse = _foodBL.GetFoodByUserId_BL(id);

            try
            {
                return Ok(foodResponse);
            }
            catch (InvalidCastException)
            {
                return NotFound(new Response { Message = "Nenhum Peso foi encontrado" });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteFood(int id)
        {
            var foodResponse = _foodBL.DeleteFood_BL(id);

            if (foodResponse != 0)
            {
                return Ok("Alimento excluido com sucesso!");
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum Alimento foi encontrado" });
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateFood([FromBody] FoodUpdateRequest food)
        {
            var foodResponse = _foodBL.UpdateFood_BL(food);

            return Ok(foodResponse);

            //if (weightResponse != null)
            //{
            //    return Ok("Peso alterado com sucesso!");
            //}
            //else
            //{
            //    return NotFound(new Response { Message = "Nenhum Peso foi encontrado" });
            //}
        }
    }
}
