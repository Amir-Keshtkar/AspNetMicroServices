using Dapper;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection connection;

        public DiscountRepository(IConfiguration configuration) {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<Coupon> GetDiscount(string productName) {
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName=@ProductName",
                 new { ProductName = productName });
            if (coupon == null)
            {
                return new Coupon
                {
                    ProductName="No Discount",
                    Description="No Discount Description",
                    Amount=0,
                };
            }
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon command)
        {
            var coupon = await connection.ExecuteAsync
                ("INSERT INTO Coupon(ProductName, Description, Amount) " +
                "Values(@ProductName, @Description, @Amount)",
                new { ProductName = command.ProductName, Description = command.Description, Amount=command.Amount});
            if (coupon == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon command)
        {
            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
                new { ProductName = command.ProductName, Description = command.Description, Amount = command.Amount, Id = command.Id });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var affected = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName=@ProductName",
                new { ProductName = productName });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
