using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon command);
        Task<bool> UpdateDiscount(Coupon command);
        Task<bool> DeleteDiscount(string productName);
    }
}
