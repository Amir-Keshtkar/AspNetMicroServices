using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Seed database associated with context {0}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order()
                {
                    UserName = "swn",
                    FirstName = "Amir",
                    LastName = "Keshtkar",
                    EmailAddress = "amir78ahkl@gmail.com",
                    AddressLine = "Qazvin",
                    Country = "Iran",
                    TotalPrice = 350,
                    CVV="4917"
                }
            };
        }
    }
}
