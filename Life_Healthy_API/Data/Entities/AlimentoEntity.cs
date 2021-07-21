using System;

namespace Life_Healthy_API.Data.Entities
{
    public class AlimentoEntity
    {
        public int AlimentoId { get; set; }
        public string Descricao { get; set; }
        public int NumCalorias { get; set; }
        public DateTime DataAlimento { get; set; }
        public int Status { get; set; }
        public int UsuarioId { get; set; }
    }
}
