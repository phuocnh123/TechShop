namespace Practice.Models
{
    public class LearnCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LearnCategory(int id, string name)
        {
            Id = id;
            Name = name;

        }
    }
}
