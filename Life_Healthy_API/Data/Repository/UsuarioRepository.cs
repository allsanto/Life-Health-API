using Dapper;
using Life_Healthy_API.Data.Entities;
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

            var query = @"INSERT INTO usuario
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

        public int GetUsuario(int id) 
        {
            using var db = Connection;

            var query = @"SELECT usuario_id,
                                 nome,
                                 data_nascimento,
                                 altura,
                                 genero,
                                 email,
                                 senha,
                                 confi_senha,
                                 status
                         FROM Usuario
                            WHERE usuario_id = @id;";

            return db.QueryFirstOrDefault<int>(query, new { id });
        }

        public UsuarioEntity GetLoginUsuario(string email, string senha)
        {
            using var db = Connection;

            var query = @"SELECT usuario_id,
                                 nome,
                                 email
                         FROM Usuario
                            WHERE email = @email
                                AND senha = @senha;";

            return db.QueryFirstOrDefault<UsuarioEntity>(query, new { email, senha });
        }

        public UsuarioEntity GetUserCheck(string email)
        {
            using var db = Connection;

            var query = @"SELECT usuario_id
                          FROM Usuario
                            WHERE email = @email";

            return db.QueryFirstOrDefault<UsuarioEntity>(query, new { email });
        }
    }
}
