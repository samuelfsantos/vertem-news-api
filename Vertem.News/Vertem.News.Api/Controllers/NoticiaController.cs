using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertem.News.Application.Queries;
using Vertem.News.Application.ViewModels;
using Vertem.News.Application.Commands;
using Vertem.News.Domain.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authorization;

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

            return GetCustomResponseSingleData(requestResult, nameof(GetById), HttpContext.Request.Path.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, InsertNoticiaViewModel model)
        {
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

            return GetCustomResponseSingleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoticiaCommand(id);
            var requestResult = await _mediator.Send(command);

            return GetCustomResponseSingleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestResult = await _mediator.Send(new GetNoticiaQuery());

            return GetCustomResponseMultipleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var requestResult = await _mediator.Send(new GetNoticiaQuery(id: id, singleData: true));

            return GetCustomResponseSingleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var requestResult = await _mediator.Send(new GetNoticiaQuery(categoria: category));

            return GetCustomResponseMultipleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("source/{source}")]
        public async Task<IActionResult> GetBySource(string source)
        {
            var requestResult = await _mediator.Send(new GetNoticiaQuery(fonte: source));

            return GetCustomResponseMultipleData(requestResult, failInstance: HttpContext.Request.Path.Value);
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> GetByKeyword(string keyword)
        {
            var requestResult = await _mediator.Send(new GetNoticiaPalavraChaveQuery(keyword));

            return GetCustomResponseMultipleData(requestResult, failInstance: HttpContext.Request.Path.Value);
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
