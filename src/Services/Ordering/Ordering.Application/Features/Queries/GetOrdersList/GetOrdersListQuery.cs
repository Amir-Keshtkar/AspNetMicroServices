using MediatR;

namespace Ordering.Application.Features.Queries.GetOrdersList
{
    public class GetOrdersListQuery: IRequest<List<OrdersVM>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }


    }
}
