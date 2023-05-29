using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertem.News.Application.Queries;
using Vertem.News.Application.ViewModels;
using Vertem.News.Application.Commands;

namespace Vertem.News.Api.Controllers
{
    [Route("api/v1/noticias")]
    [ApiController]
    public class NoticiaController : BaseController
    {
        private readonly IMediator _mediator;

        public NoticiaController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
