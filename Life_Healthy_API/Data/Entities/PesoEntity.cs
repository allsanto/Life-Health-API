using System;

namespace Life_Healthy_API.Data.Entities
{
    public class PesoEntity
    {
        public int PesoId { get; set; }
        public DateTime DataPeso { get; set; }
        public decimal Peso { get; set; }
        public int Status { get; set; }
        public int UsuarioId { get; set; }
    }
}
