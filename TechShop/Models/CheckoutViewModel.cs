namespace TechShop.Models
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public Dictionary<int, string> ShippingMethod { get; set; }
    }
}
