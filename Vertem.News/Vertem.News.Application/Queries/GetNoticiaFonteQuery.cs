using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Queries
{
    public class GetNoticiaFonteQuery : IQuery<RequestResult<NoticiaOutput>>
    {
        public string Fonte { get; private set; }

        public GetNoticiaFonteQuery(string fonte)
        {
            Fonte = fonte;
        }
    }
}
