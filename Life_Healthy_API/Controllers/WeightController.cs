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
        [Route("weight")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult InsertWeight([FromBody] WeightRequest weightRequest)
        {
            var response = _weightBL.InsertWeight_BL(weightRequest);

            if (response != 0)
            {
                return Ok("Peso inserido com Sucesso!");
            }
            else
            {
                return NotFound(new Response { Message = "Usuário não encontrado!" });
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
        [ProducesResponseType(typeof(WeightResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetWeightById(int id)
        {
            var weightResponse = _weightBL.GetWeightById_BL(id);

            if (weightResponse != null)
            {
                return Ok(weightResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum peso encontrado." });
            }
        }

        [HttpGet]
        [Route("getgetWeightByUserId/{id}")]
        public IActionResult GetWeightByUserId(int id)
        {
            var weightResponse = _weightBL.GetWeightByUserId_BL(id);

            try
            {
                return Ok(weightResponse);
            }
            catch (InvalidCastException)
            {
                return NotFound(new Response { Message = "Nenhum Peso foi encontrado" });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteWeight(int id)
        {
            var weightResponse = _weightBL.DeleteWheight_BL(id);

            if (weightResponse != 0)
            {
                return Ok("Peso excluido com sucesso!");
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum Peso foi encontrado" });
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateWeight([FromBody] WeightUpdateRequest weight)
        {
            var weightResponse = _weightBL.UpdateWeight_BL(weight);

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
