using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services {
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger) {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context) {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if(coupon == null) {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found!"));
            }
            _logger.LogInformation($"Discount is retrieved for ProductName :{0}, Amount: {1}", coupon.ProductName, coupon.Amount);
            var couponModel = new CouponModel {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
                Id = coupon.Id,
            };
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context) {
            var coupon = new Coupon {
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                Id = request.Coupon.Id,
            };
            await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation($"Discount is Successfully created. ProductName: {0}", coupon.ProductName);
            var couponModel = new CouponModel {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName,
            };
            return couponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context) {
            var coupon = new Coupon {
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                Id = request.Coupon.Id,
            };
            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount is Successfully updated. ProductName: {0}", coupon.ProductName);
            var couponModel = new CouponModel {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName,
            };
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context) {
            var deleteResponse=await _discountRepository.DeleteDiscount(request.ProductName);
            var response=new DeleteDiscountResponse {
                Success=deleteResponse,
            };
            return response;
        }
    }
}
