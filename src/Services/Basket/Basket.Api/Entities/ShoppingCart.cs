  namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }=new List<ShoppingCartItem>();
        public ShoppingCart()
        {
        }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totall = 0;
                foreach (var cart in Items)
                {
                    totall += cart.Price * cart.Quantity;
                }
                return totall;
            }
        }
    }
}
