using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Commands
{
    public class InsertNoticiaIntegracaoNewsApiOrgCommand : ICommand<RequestResult<NoticiaOutput>>
    {
        public InsertNoticiaIntegracaoNewsApiOrgCommand()
        {
        }
    }
}
