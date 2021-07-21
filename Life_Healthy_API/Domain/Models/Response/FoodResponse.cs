using System;

namespace Life_Healthy_API.Domain.Models.Response
{
    public class FoodResponse
    {
        public int FoodId { get; set; }
        public string Description { get; set; }
        public int NumCalories { get; set; }
        public DateTime DateFood { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
    }
}
