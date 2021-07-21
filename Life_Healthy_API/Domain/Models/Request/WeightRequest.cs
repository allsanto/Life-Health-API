using System;

namespace Life_Healthy_API.Domain.Models.Request
{
    public class WeightRequest
    {
        public DateTime DateWeigth { get; set; }
        public decimal Weigth { get; set; }
        public int UserId { get; set; }
    }
}
