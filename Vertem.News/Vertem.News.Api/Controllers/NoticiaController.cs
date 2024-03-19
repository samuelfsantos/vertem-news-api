using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertem.News.Application.Queries;
using Vertem.News.Application.ViewModels;
using Vertem.News.Application.Commands;
using Vertem.News.Domain.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authorization;
using Vertem.News.Api.Common;
using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Responses;
using Vertem.News.Services.NewsApiOrg.Models;

namespace Vertem.News.Api.Controllers
{
    [Route("api/v1/news")]
    [ApiController]
    public class NoticiaController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;

        public NoticiaController(IMediator mediator, IDistributedCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertNoticiaViewModel model)
        {
            var command = new InsertNoticiaCommand(
                            model.Titulo,
                            model.Descricao,
                            model.Conteudo,
                            model.Categoria,
                            model.Fonte,
                            model.DataPublicacao,
                            model.ImgUrl,
                            model.Autor);
            var requestResult = await _mediator.Send(command);

            await _cache.RemoveAsync("ObterTodasNoticias");
            await _cache.RemoveAsync("ObterNoticiasPelaCategoria");
            await _cache.RemoveAsync("ObterNoticiasPelaFonte");
            await _cache.RemoveAsync("ObterNoticiasPelaPalavraChave");

            return GetCustomResponseSingleData(requestResult, nameof(GetById), HttpContext.Request.Path.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, InsertNoticiaViewModel model)
        {
            string cacheKey = $"ObterNoticiaPeloId-{id}";

            var command = new UpdateNoticiaCommand(
                            id,
                            model.Titulo,
                            model.Descricao,
                            model.Conteudo,
                            model.Categoria,
                            model.Fonte,
                            model.DataPublicacao,
                            model.ImgUrl,
                            model.Autor);
            var requestResult = await _mediator.Send(command);
            await _cache.RemoveAsync(cacheKey);

            return GetCustomResponseSingleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string cacheKey = $"ObterNoticiaPeloId-{id}";

            var command = new DeleteNoticiaCommand(id);
            var requestResult = await _mediator.Send(command);
            await _cache.RemoveAsync(cacheKey);

            return GetCustomResponseSingleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //const string cacheKey = "ObterTodasNoticias";
                //var noticiasEmCache = await _cache.GetCachedItemAsync<NoticiaOutput>(cacheKey);

                //if (noticiasEmCache == null)
                //{
                //    var results = await _mediator.Send(new GetNoticiaQuery());
                //    await _cache.SaveItemAsync(results, cacheKey, expirationInSeconds: 20);

                //    return GetCustomResponseMultipleData(results, failInstance: HttpContext.Request.Path.Value);
                //}
                //else
                //    return GetCustomResponseMultipleData(noticiasEmCache, failInstance: HttpContext.Request.Path.Value);

                const string cacheKey = "ObterTodasNoticias";
                var noticiasEmCache = await _cache.GetCachedGenericItemAsync<IEnumerable<NoticiaOutput>>(cacheKey);

                if (noticiasEmCache is null)
                {
                    var results = await _mediator.Send(new GetNoticiaQuery());
                    await _cache.SaveGenericItemAsync(results.MultipleData, cacheKey, expirationInSeconds: 20);

                    return Ok(new
                    {
                        EstaCacheado = false,
                        Results = results.MultipleData
                    });
                }
                else
                    return Ok(new
                    {
                        EstaCacheado = true,
                        Results = noticiasEmCache
                    });

            }
            catch (Exception ex)
            {
                var respostaErro = new { MensagemErro = ex.Message, Error = ex.ToString() };

                return BadRequest(respostaErro);
            }            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string cacheKey = $"ObterNoticiaPeloId-{id}";
            var noticiasEmCache = await _cache.GetCachedItemAsync<NoticiaOutput>(cacheKey);

            if (noticiasEmCache is null)
            {
                var results = await _mediator.Send(new GetNoticiaQuery(id: id, singleData: true));
                await _cache.SaveItemAsync(results, cacheKey, expirationInSeconds: 20);

                return GetCustomResponseSingleData(results, failInstance: HttpContext.Request.Path.Value);
            }
            else
                return GetCustomResponseSingleData(noticiasEmCache, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            const string cacheKey = "ObterNoticiasPelaCategoria";
            var noticiasEmCache = await _cache.GetCachedItemAsync<NoticiaOutput>(cacheKey);

            if (noticiasEmCache is null)
            {
                var results = await _mediator.Send(new GetNoticiaQuery(categoria: category));
                await _cache.SaveItemAsync(results, cacheKey, expirationInSeconds: 20);

                return GetCustomResponseMultipleData(results, failInstance: HttpContext.Request.Path.Value);
            }
            else
                return GetCustomResponseMultipleData(noticiasEmCache, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("source/{source}")]
        public async Task<IActionResult> GetBySource(string source)
        {
            const string cacheKey = "ObterNoticiasPelaFonte";
            var noticiasEmCache = await _cache.GetCachedItemAsync<NoticiaOutput>(cacheKey);

            if (noticiasEmCache is null)
            {
                var results = await _mediator.Send(new GetNoticiaQuery(fonte: source));
                await _cache.SaveItemAsync(results, cacheKey, expirationInSeconds: 20);

                return GetCustomResponseMultipleData(results, failInstance: HttpContext.Request.Path.Value);
            }
            else
                return GetCustomResponseMultipleData(noticiasEmCache, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> GetByKeyword(string keyword)
        {
            const string cacheKey = "ObterNoticiasPelaPalavraChave";
            var noticiasEmCache = await _cache.GetCachedItemAsync<NoticiaOutput>(cacheKey);

            if (noticiasEmCache is null)
            {
                var results = await _mediator.Send(new GetNoticiaPalavraChaveQuery(keyword));
                await _cache.SaveItemAsync(results, cacheKey, expirationInSeconds: 20);

                return GetCustomResponseMultipleData(results, failInstance: HttpContext.Request.Path.Value);
            }
            else
                return GetCustomResponseMultipleData(noticiasEmCache, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpPost("integracao-news-api-org")]
        public async Task<IActionResult> InsertIntegracaoNewsApiOrg()
        {
            var command = new InsertNoticiaIntegracaoNewsApiOrgCommand();
            var requestResult = await _mediator.Send(command);

            return GetCustomResponseSingleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }





        [AllowAnonymous]
        [HttpGet("redis")]
        public IActionResult GetRedisCache(string key)
        {
            try
            {
                var cacheValue = _cache.GetString(key);

                if (cacheValue == null)
                {
                    return NotFound();
                }

                return Ok(cacheValue);
            }
            catch (Exception ex)
            {
                var respostaErro = new { MensagemErro = ex.Message, Error = ex.ToString() };

                return BadRequest(respostaErro);
            }

        }

        [AllowAnonymous]
        [HttpPost("redis")]
        public IActionResult SetRedisCache(string key, string value)
        {
            try
            {
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10) // Expira em 10 segundos
                };

                _cache.SetString(key, value, cacheOptions);

                return Ok();
            }
            catch (Exception ex)
            {
                var respostaErro = new { MensagemErro = ex.Message, Error = ex.ToString() };

                return BadRequest(respostaErro);
            }
        }


    }
}
