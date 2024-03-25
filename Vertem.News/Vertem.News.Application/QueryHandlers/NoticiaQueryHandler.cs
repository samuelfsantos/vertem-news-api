using MediatR;
using Vertem.News.Domain.Outputs;
using Vertem.News.Application.Queries;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.Responses;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
using Vertem.News.Application.Extensions;
using Vertem.News.Domain.Entities;

namespace Vertem.News.Application.QueryHandlers
{
    public class NoticiaQueryHandler : IRequestHandler<GetNoticiaQuery, RequestResult<NoticiaOutput>>, IRequestHandler<GetNoticiaPalavraChaveQuery, RequestResult<NoticiaOutput>>
    {
        private readonly IDistributedCache _cache;
        private readonly IGenericRepository<Noticia> _repository;
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaQueryHandler(IDistributedCache cache, IGenericRepository<Noticia> repository, INoticiaRepository noticiaRepository)
        {
            _cache = cache;
            _repository = repository;
            _noticiaRepository = noticiaRepository;
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(GetNoticiaFonteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                const string cacheKey = "ObterNoticiasPorFonte";

                var outputEmCache = await _cache.GetCacheAsync<NoticiaOutput>(cacheKey);
                if (outputEmCache is null)
                {
                    var noticias = await _noticiaRepository.ObterPorFonteAsync(request.Fonte);
                    var output = noticias.Select(n => NoticiaOutput.FromEntity(n));
                    await _cache.SaveCacheAsync(output, cacheKey, expirationInSeconds: 20);

                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
                }
                else
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, outputEmCache, Enumerable.Empty<ErrorModel>());
            }
            catch
            {
                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(GetNoticiaPalavraChaveQuery request, CancellationToken cancellationToken)
        {
            try
            {
                const string cacheKey = "ObterNoticiasPorPalavraChave";

                var outputEmCache = await _cache.GetCacheAsync<NoticiaOutput>(cacheKey);
                if (outputEmCache is null)
                {
                    var noticias = await _noticiaRepository.ObterPorPalavraChaveAsync(request.PalavraChave);
                    var output = noticias.Select(n => NoticiaOutput.FromEntity(n));
                    await _cache.SaveCacheAsync(output, cacheKey, expirationInSeconds: 20);

                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
                }
                else
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, outputEmCache, Enumerable.Empty<ErrorModel>());
            }
            catch
            {
                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(GetNoticiaCategoriaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                const string cacheKey = "ObterNoticiasPorCategoria";

                var outputEmCache = await _cache.GetCacheAsync<NoticiaOutput>(cacheKey);
                if (outputEmCache is null)
                {
                    var noticias = await _noticiaRepository.ObterPorCategoriaAsync(request.Categoria);
                    var output = noticias.Select(n => NoticiaOutput.FromEntity(n));
                    await _cache.SaveCacheAsync(output, cacheKey, expirationInSeconds: 20);

                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
                }
                else
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, outputEmCache, Enumerable.Empty<ErrorModel>());
            }
            catch
            {
                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(GetNoticiaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //if (request.SingleData)
                //{
                //    var cacheKey = $"ObterNoticias-{request.Id}";

                //    var outputEmCache = await _cache.GetCacheAsync<NoticiaOutput>(cacheKey);
                //    if (outputEmCache is null)
                //    {
                //        var noticia = await _repository.ObterAsync(request.Id.Value);
                //        if (noticia == null)
                //            return new RequestResult<NoticiaOutput>(HttpStatusCode.NotFound, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());

                //        var output = NoticiaOutput.FromEntity(noticia);
                //        await _cache.SaveCacheAsync(output, cacheKey, expirationInSeconds: 20);

                //        return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
                //    }
                //    else
                //        return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, outputEmCache, Enumerable.Empty<ErrorModel>());
                //}
                //else
                //{
                //    const string cacheKey = "ObterTodasNoticias";

                //    var outputEmCache = await _cache.GetCacheAsync<NoticiaOutput>(cacheKey);
                //    if (outputEmCache is null)
                //    {
                //        var noticias = _repository.ObterTodos().ToList();
                //        var output = noticias.Select(n => NoticiaOutput.FromEntity(n));
                //        await _cache.SaveCacheAsync(output, cacheKey, expirationInSeconds: 20);

                //        return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
                //    }
                //    else
                //        return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, outputEmCache, Enumerable.Empty<ErrorModel>());
                //}


                const string cacheKey = "ObterTodasNoticias";

                var outputEmCache = await _cache.GetCacheAsync<NoticiaOutput>(cacheKey);
                if (outputEmCache is null)
                {
                    var noticias = _repository.ObterTodos().ToList();
                    var output = noticias.Select(n => NoticiaOutput.FromEntity(n));
                    await _cache.SaveCacheAsync(output, cacheKey, expirationInSeconds: 20);

                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, output, Enumerable.Empty<ErrorModel>());
                }
                else
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, outputEmCache, Enumerable.Empty<ErrorModel>());

            }
            catch(Exception ex)
            {
                var erros = new List<ErrorModel>();
                erros.Add(new ErrorModel("Exception", ex.Message));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), erros);
            }
            //catch
            //{
            //    return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            //}
        }
    }
}
