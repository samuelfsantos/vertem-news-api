using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Queries
{
    public class GetNoticiaSemCacheQuery : IQuery<RequestResult<NoticiaOutput>>
    {
        public Guid? Id { get; private set; }

        public bool SingleData { get; private set; }

        public GetNoticiaSemCacheQuery(Guid? id = null, bool singleData = false)
        {
            Id = id;

            SingleData = singleData;
        }
    }
}
