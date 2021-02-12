using Dapper;
using Life_Healthy_API.Data.Entities;
using Life_Healthy_API.Domain.Models;
using Microsoft.Extensions.Configuration;


namespace Life_Healthy_API.Data.Repository
{
    public class UsuarioRepository : RepositoryBase
    {
        public UsuarioRepository(IConfiguration configuration) 
        {
            base.configuration = configuration;
        }

        #region Método POST - Inserir Usuario
        public int InserirUsuario(UsuarioEntity usuario) 
        {
            using var db = Connection;

            var query = @"INSERT INTO usuarios
                                (nome,
                                 data_nascimento,
                                 altura,
                                 genero,
                                 email,
                                 senha,
                                 confi_senha)
                         VALUES (@Nome,
                                 @DataNascimento,
                                 @Altura,
                                 @Genero,
                                 @Email,
                                 @Senha,
                                 @ConfiSenha)
                            RETURNING usuario_id;";

            return db.ExecuteScalar<int>(query, new 
            { 
                usuario.Nome,
                usuario.DataNascimento,
                usuario.Altura,
                usuario.Genero,
                usuario.Email,
                usuario.Senha,
                usuario.ConfiSenha
            });
        }
        #endregion

        public UsuarioEntity GetUsuario(int id) 
        {
            using var db = Connection;

            var query = @"SELECT usuario_id
                                 nome,
                                 data_nascimento,
                                 altura
                                 genero,
                                 email,
                                 senha,
                                 confi_senha,
                                 status
                         FROM Usuarios
                            WHERE usuario_id = @email;";

            return db.QueryFirstOrDefault<UsuarioEntity>(query, new { id });
        }

        public UsuarioEntity GetLoginUsuario(string email, string senha)
        {
            using var db = Connection;

            var query = @"SELECT usuario_id,
                                 nome,
                                 email
                         FROM Usuarios
                            WHERE email = @email
                                AND senha = @senha;";

            return db.QueryFirstOrDefault<UsuarioEntity>(query, new { email, senha });
        }
    }
}
