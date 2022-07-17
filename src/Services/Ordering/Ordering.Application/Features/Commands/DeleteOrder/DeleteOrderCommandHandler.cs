using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommand> _logger;

        public DeleteOrderCommandHandler(ILogger<DeleteOrderCommand> logger, IOrderRepository orderRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete= await _orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
            {
                //_logger.LogError($"Order with Id: {request.Id} not found");
                throw new NotFoundException(nameof(Order), request.Id);
            }
            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");
            return Unit.Value;
        }
    }
}
