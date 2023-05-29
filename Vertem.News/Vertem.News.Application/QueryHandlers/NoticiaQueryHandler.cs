using MediatR;
using Vertem.News.Domain.Outputs;
using Vertem.News.Application.Queries;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.Responses;
using System.Net;

namespace Vertem.News.Application.QueryHandlers
{
    public class NoticiaQueryHandler : IRequestHandler<GetNoticiaQuery, RequestResult<NoticiaOutput>>
    {
        private readonly INoticiaRepository _repository;

        public NoticiaQueryHandler(INoticiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(GetNoticiaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var noticias = await _repository.Select(
                                request.Id, 
                                request.Titulo, 
                                request.Descricao, 
                                request.Conteudo, 
                                request.Categoria, 
                                request.Fonte,
                                request.DataPublicacao,
                                request.ImgUrl,
                                request.Autor,
                                true);
                if (request.SingleData)
                {
                    var noticia = noticias.FirstOrDefault();
                    if (noticia == null)
                        return new RequestResult<NoticiaOutput>(HttpStatusCode.NotFound, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());

                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, NoticiaOutput.FromEntity(noticia), Enumerable.Empty<ErrorModel>());
                }
                var output = noticias.Select(p => NoticiaOutput.FromEntity(p));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
            }
            catch
            {
                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            }
        }
    }
}
