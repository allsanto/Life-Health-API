namespace Life_Healthy_API.Domain.Models.Request
{
    public class FoodUpdateRequest : FoodRequest
    {
        public int? FoodId { get; set; }
    }
}
