namespace Practice.Models
{
    public class LearnProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public LearnProduct(int id, string name, decimal price, int categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;

        }
    }
}
