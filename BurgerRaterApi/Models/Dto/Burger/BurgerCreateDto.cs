namespace BurgerRaterApi.Models.Dto.Burger
{
    public class BurgerCreateDto
    {
        public string Name { get; set; }

        public string Ingredients { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public int MenuId { get; set; }
    }
}
