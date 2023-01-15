namespace Shared.JsonModels
{
    public class OrdersCollection
    {
        public IEnumerable<Order> Orders { get; set; }

        public static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Item = "Duff Beer",
                    CustomerName = "Homer Simpson",
                    Count = 4,
                    Price = new Price
                    {
                        TotalPrice = 12.50M,
                        Discount = true
                    },
                },
                new Order
                {
                    Id = 2,
                    Item = "Cherry Pie",
                    CustomerName = "Marge Simpson",
                    Count = 1,
                    Price = new Price
                    {
                        TotalPrice = 5.15M,
                        Discount = false
                    }
                }
            };
        }
    }
}