using System;

namespace Life_Healthy_API.Domain.Models.Request
{
    public class FoodRequest
    {
        public string Description { get; set; }
        public int NumCalories { get; set; }
        public DateTime DateFood{ get; set; }
        public int UserId { get; set; }
    }
}
