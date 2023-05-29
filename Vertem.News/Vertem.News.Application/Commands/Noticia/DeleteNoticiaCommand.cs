using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Commands
{
    public class DeleteNoticiaCommand : ICommand<RequestResult<NoticiaOutput>>
    {
        public Guid Id { get; private set; }

        public DeleteNoticiaCommand(Guid id)
        {
            Id = id;
        }
    }
}
