using Vertem.News.Infra.Messages;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Vertem.News.Infra.PipelineBehavior
{
    [ExcludeFromCodeCoverage]
    public class ShallowValidationBehavior<TRequest, TResponse> : ValidationBehavior<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
        where TResponse : class
    {
        public ShallowValidationBehavior(IEnumerable<IShallowValidator<TRequest>> validators) : base(validators, HttpStatusCode.BadRequest)
        {
        }
    }
}
