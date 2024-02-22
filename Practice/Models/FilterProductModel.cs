namespace Practice.Models
{
    public class FilterProductModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string KeyWord { get; set; }
        public int? CategoryId { get; set; }
    }
}
