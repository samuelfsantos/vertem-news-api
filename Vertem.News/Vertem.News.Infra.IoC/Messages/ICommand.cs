using MediatR;

namespace Vertem.News.Infra.Messages
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
