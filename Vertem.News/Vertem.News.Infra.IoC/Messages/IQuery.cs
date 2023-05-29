using MediatR;

namespace Vertem.News.Infra.Messages
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
