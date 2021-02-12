using AutoMapper;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Data.Repository;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Domain.Models.Response;

namespace Life_Healthy_API.Business
{
    public class UsuarioBL
    {
        private readonly IMapper _mapper;
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioBL(IMapper mapper, UsuarioRepository usuarioRepository) 
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        #region Método POST - Inserir Usuario
        public int InsertUserBL(UsuarioRequest userReq) 
        {
            var usuarioEntity = _mapper.Map<UsuarioEntity>(userReq);

            var idUsuario = _usuarioRepository.InserirUsuario(usuarioEntity);
            return idUsuario;
        }

        public UsuarioResponse GetById(int id) 
        {
            var usuarioEntity = _usuarioRepository.GetUsuario(id);
            var alunoResponse = _mapper.Map<UsuarioResponse>(usuarioEntity);

            return alunoResponse;
        }
        #endregion

        public UserLoginResponse GetUserLogin(UserLoginRequest user)
        {
            var userEntity = _usuarioRepository.GetLoginUsuario(user.Email, user.Senha);
            var usuarioResponse = _mapper.Map<UserLoginResponse>(userEntity);

            return usuarioResponse;
        }
    }
}
