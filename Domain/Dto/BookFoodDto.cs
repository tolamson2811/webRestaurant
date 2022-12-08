namespace Domain.Dto
{
    public class BookFoodDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public string ImageLink { get; set; }
        public int Quantity { get; set; }
    }
}
