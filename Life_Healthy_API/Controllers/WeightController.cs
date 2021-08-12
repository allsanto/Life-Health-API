using Life_Healthy_API.Business;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Life_Healthy_API.Controllers
{
    [Route("apilife/weight")]
    public class WeightController : Controller
    {
        private readonly WeightBL _weightBL;
        public WeightController(WeightBL weightBL)
        {
            _weightBL = weightBL;
        }

        /// <summary>
        /// Inserir Peso
        /// </summary>
        /// <param name="weightRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult InsertWeight([FromBody] WeightRequest weightRequest)
        {
            var response = _weightBL.InsertWeight_BL(weightRequest);

            if (response == 1)
            {
                return NotFound(new Errors { errors = "Usuário não encontrado!" });
            }
            else
            {
                return Ok(new Response { Message = "Peso inserido com Sucesso!" });
            }
        }

        /// <summary>
        /// Retorna o peso pelo peso_id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getWeightById/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(WeightResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public IActionResult GetWeightById(int id)
        {
            var weightResponse = _weightBL.GetWeightById_BL(id);

            if (weightResponse != null)
            {
                return Ok(weightResponse);
            }
            else
            {
                return NotFound(new Errors { errors = "Nenhum peso encontrado." });
            }
        }

        /// <summary>
        /// Retorna pesos pelo ID do Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getWeightByUserId/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(WeightResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public IActionResult GetWeightByUserId(int id)
        {
            var weightResponse = _weightBL.GetWeightByUserId_BL(id);

            try
            {
                return Ok(weightResponse);
            }
            catch (InvalidCastException)
            {
                return NotFound(new Errors { errors = "Nenhum Peso foi encontrado" });
            }
        }

        /// <summary>
        /// Deleta um peso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public IActionResult DeleteWeight(int id)
        {
            var weightResponse = _weightBL.DeleteWheight_BL(id);

            if (weightResponse != 0)
            {
                return Ok(new Response { Message = "Peso excluido com sucesso!" });
            }
            else
            {
                return NotFound(new Errors { errors = "Nenhum Peso foi encontrado" });
            }
        }

        /// <summary>
        /// Atualiza peso
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public IActionResult UpdateWeight([FromBody] WeightUpdateRequest weight)
        {
            var weightResponse = _weightBL.UpdateWeight_BL(weight);

            if (weightResponse.Message == "1")
            {
                return Ok(new Response { Message = "Peso atualizado com sucesso!" });
            }

            return Ok(weightResponse);

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
