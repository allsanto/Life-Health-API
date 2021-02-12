using Life_Healthy_API.Business;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Domain.Models;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;
using Life_Healthy_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life_Healthy_API.Controllers
{
    [Route("apilife/users")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioBL _usuarioBL;
        public UsuarioController(UsuarioBL usuarioBL) 
        {
            _usuarioBL = usuarioBL;
        }

        /// <summary>
        /// Cadastro de Usuarios
        /// </summary>
        /// <param name="usuarioRequest">JSON</param>
        /// <returns>JSON</returns>
        // Função para cadastrar Usuarios
        [HttpPost]
        [Route("insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UsuarioRequest usuarioRequest) 
        {
            var idUsuario = _usuarioBL.InsertUserBL(usuarioRequest);
            return CreatedAtAction(nameof(GetById), new { id = idUsuario }, usuarioRequest);
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetById(int id)
        {
            var usuarioResponse = _usuarioBL.GetById(id);

            if (usuarioResponse != null)
            {
                return Ok(usuarioResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum usuario foi encontrado." });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserLoginRequest userLoginRequest)
        {
            // Recupera o usuário
            // var usuarioResponse = _usuarioBL.GetById(id);
            var user = _usuarioBL.GetUserLogin(userLoginRequest);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(userLoginRequest);

            // Oculta a senha
            userLoginRequest.Senha = "";

            // Retorna os dados
            return new
            {
                userLogin = user,
                token = token
            };
        }

        //[HttpPost]
        //[Route("login")]
        //[AllowAnonymous]
        //public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        //{
        //    // Recupera o usuário
        //    // var usuarioResponse = _usuarioBL.GetById(id);
        //    var user = _usuarioBL.Login(model);

        //    // Verifica se o usuário existe
        //    if (user == null)
        //        return NotFound(new { message = "Usuário ou senha inválidos" });

        //    // Gera o Token
        //    var token = TokenService.GenerateToken(user);

        //    // Oculta a senha
        //    user.Senha = "";

        //    // Retorna os dados
        //    return new
        //    {
        //        user = user,
        //        token = token
        //    };
        //}

    }
}
