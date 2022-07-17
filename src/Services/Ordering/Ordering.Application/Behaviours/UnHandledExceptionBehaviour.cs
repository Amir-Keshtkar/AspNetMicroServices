using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviours
{
    public class UnHandledExceptionBehaviour<TRequest, TRespons> : IPipelineBehavior<TRequest, TRespons> where TRequest : IRequest<TRespons>
    {
        private readonly ILogger<TRequest> _logger;

        public UnHandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TRespons> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TRespons> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }

        }
    }
}
