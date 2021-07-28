using System;

namespace Life_Healthy_API.Domain.Models.Response
{
    public class UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Altura { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public string ConfiSenha { get; set; }
    }
}
