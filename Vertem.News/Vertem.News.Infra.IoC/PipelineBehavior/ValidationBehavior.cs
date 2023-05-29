using FluentValidation;
using MediatR;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;
using System.Net;

namespace Vertem.News.Infra.PipelineBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly HttpStatusCode _statusCode;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, HttpStatusCode statusCode)
        {
            _validators = validators;
            _statusCode = statusCode;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);
            var validationFailures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .GroupBy(x => x.ErrorMessage)
                .Select(x => x.First())
                .Select(x => new ErrorModel(x.ErrorCode, x.ErrorMessage));

            if (validationFailures.Any())
            {
                var responseType = typeof(TResponse);
                var resultType = responseType.GetGenericArguments().First();
                var invalidResponseType = typeof(RequestResult<>).MakeGenericType(resultType);

                var responseObj = Activator.CreateInstance(invalidResponseType,
                                                           _statusCode,
                                                           validationFailures) as TResponse;
                return responseObj;
            }
            return await next();
        }
    }
}
