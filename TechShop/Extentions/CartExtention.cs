using Newtonsoft.Json;
using TechShop.Models;

namespace TechShop.Extentions
{
    public static class CartExtention
    {
        public static void SetCart(this ISession session, string key, List<CartItemViewModel> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static List<CartItemViewModel> GetCart(this ISession session, string key)
        {
            var value = session.GetString(key);
            var result = value != null ? JsonConvert.DeserializeObject<List<CartItemViewModel>>(value) : null;
            return result;
        }
    }
}
