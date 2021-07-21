using System;

namespace Life_Healthy_API.Domain.Models.Response
{
    public class WeightResponse
    {
        public int WeightId { get; set; }
        public DateTime DateWeigth { get; set; }
        public decimal Weigth { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
    }
}
