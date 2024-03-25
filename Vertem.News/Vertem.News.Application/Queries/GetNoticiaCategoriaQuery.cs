using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Queries
{
    public class GetNoticiaCategoriaQuery : IQuery<RequestResult<NoticiaOutput>>
    {
        public string Categoria { get; private set; }

        public GetNoticiaCategoriaQuery(string categoria)
        {
            Categoria = categoria;
        }
    }
}
