using AutoMapper;
using EventBus.Massages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Commands.CheckOutOrder;

namespace Ordering.Api.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(ILogger<BasketCheckoutConsumer> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var comman = _mapper.Map<CheckOutOrderCommand>(context.Message);
            var result = await _mediator.Send(comman);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id: {newOrderId}", result);

        }
    }
}
