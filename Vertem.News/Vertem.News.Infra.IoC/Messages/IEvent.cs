using MediatR;

namespace Vertem.News.Infra.Messages
{
    public interface IEvent : IRequest<bool>
    {
    }
}
