using Life_Healthy_API.Business;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;
using Life_Healthy_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMatrix.WebData;
using Windows.ApplicationModel.Email;

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
        /// Fazer Login
        /// </summary>
        /// <param name="userLoginRequest">JSON</param>
        /// <returns>JSON</returns>
        // Fazer Login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] UserLoginRequest userLoginRequest)
        {
            var user = _usuarioBL.GetUserLogin(userLoginRequest);

            if (user == null)
                return NotFound(new Errors { errors = "Usuário ou senha inválidos." });

            var token = TokenService.GenerateToken(userLoginRequest);

            return new
            {
                userLogin = user,
                accessToken = token
            };
        }

        /// <summary>
        /// Cadastrar alunos
        /// </summary>
        /// <param name="usuarioRequest">JSON</param>
        /// <returns>JSON</returns>
        // Função para cadastro de aluno
        [HttpPost]
        [Route("insert")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UsuarioRequest usuarioRequest) 
        {
            var userCheck = _usuarioBL.VerificaSeUsuarioExiste(usuarioRequest.Email);

            if (userCheck != null)
            {
                return BadRequest(new Errors { errors = "Usuario ja cadastrado." });
            }
            try
            {
                var idUsuario = _usuarioBL.InsertUserBL(usuarioRequest);
                //return CreatedAtAction(nameof(GetById), new { id = idUsuario }, usuarioRequest);
                return Ok(new Response { Message = "Usuario cadastrado com sucesso!." });
            }
            catch (System.Exception)
            {
                return BadRequest( new Errors { errors = "Erro sistemico contate o administrador!" });
            }
        }

        /// <summary>
        /// Retorna o usuario por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{id}")]
        [AllowAnonymous]
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
                return NotFound(new Errors { errors = "Nenhum usuario foi encontrado." });
            }
        }

        [HttpPost]
        public ActionResult ForgotPassword(string UserName)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(UserName))
                {
                    string To = UserName, UserID, Password, SMTPPort, Host;
                    string token = WebSecurity.GeneratePasswordResetToken(UserName);
                    if (token == null)
                    {
                        // If user does not exist or is not confirmed.
                        return View("Index");
                    }
                    else
                    {
                        //Create URL with above token
                        var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = UserName, code = token }, "http") + "'>Reset Password</a>";
                        //HTML Template for Send email
                        string subject = "Your changed password";
                        string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                        //Get and set the AppSettings using configuration manager.
                        EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);
                        //Call send email methods.
                        EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);
                    }
                }
            }
            return View();
        }
    }
}