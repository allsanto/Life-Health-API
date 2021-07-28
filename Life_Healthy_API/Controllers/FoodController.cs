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
    public class FoodController : Controller
    {
        private readonly FoodBL _foodBL;
        public FoodController(FoodBL food)
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

            if (response == 1)
            {
                return NotFound(new Response { Message = "Usuário não encontrado!" });
            }
            else
            {
                return Ok("Alimento inserido com sucesso!");
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

        /// <summary>
        /// Retorna todos alimentos de um Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getFoodByUserId/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(FoodResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
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

        /// <summary>
        /// Deleta alimento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
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

        /// <summary>
        /// Atualiza alimento
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
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
