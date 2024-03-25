using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Queries
{
    public class GetNoticiaPalavraChaveQuery : IQuery<RequestResult<NoticiaOutput>>
    {
        public string PalavraChave { get; private set; }

        public GetNoticiaPalavraChaveQuery(string palavraChave)
        {
            PalavraChave = palavraChave;
        }
    }
}
