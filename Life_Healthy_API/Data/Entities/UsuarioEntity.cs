using System;

namespace Life_Healthy_API.Data.Entities
{
    public class UsuarioEntity
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Altura { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfiSenha { get; set; }
        public int Status { get; set; }
    }
}