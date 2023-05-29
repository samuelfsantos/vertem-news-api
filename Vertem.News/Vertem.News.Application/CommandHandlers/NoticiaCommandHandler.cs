using MediatR;
using Vertem.News.Application.Commands;
using Vertem.News.Domain.Entities;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Responses;
using System.Net;
using Vertem.News.Domain.Enums;

namespace Vertem.News.Application.CommandHandlers
{
    public class NoticiaCommandHandler : IRequestHandler<InsertNoticiaCommand, RequestResult<NoticiaOutput>>,
                                        IRequestHandler<UpdateNoticiaCommand, RequestResult<NoticiaOutput>>,
                                        IRequestHandler<DeleteNoticiaCommand, RequestResult<NoticiaOutput>>,
                                        IRequestHandler<InsertNoticiaIntegracaoNewsApiOrgCommand, RequestResult<NoticiaOutput>>
    {
        private readonly INoticiaRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly INewsApiOrgService _newsApiOrgService;
        private readonly List<ErrorModel> _errors;

        public NoticiaCommandHandler(INoticiaRepository noticiaRepository, IUnitOfWork uow, INewsApiOrgService newsApiOrgService)
        {
            _repository = noticiaRepository;
            _uow = uow;
            _newsApiOrgService = newsApiOrgService;
            _errors = new List<ErrorModel>();
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(InsertNoticiaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var noticia = new Noticia(
                                request.Titulo,
                                request.Descricao,
                                request.Conteudo,
                                request.Categoria,
                                request.Fonte,
                                request.DataPublicacao,
                                request.ImgUrl,
                                request.Autor);

                noticia = await _repository.Insert(noticia);
                await _uow.Commit();

                return new RequestResult<NoticiaOutput>(HttpStatusCode.Created, NoticiaOutput.FromEntity(noticia), Enumerable.Empty<ErrorModel>());
            }
            catch (Exception)
            {
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante o cadastro da notícia de título {request.Titulo}"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(UpdateNoticiaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var noticia = (await _repository.Select(id: request.Id)).FirstOrDefault();
                if (noticia == null)
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.NotFound, default(NoticiaOutput), _errors);

                noticia.UpdateTitulo(request.Titulo);
                noticia.UpdateDescricao(request.Descricao);
                noticia.UpdateConteudo(request.Conteudo);
                noticia.UpdateCategoria(request.Categoria);
                noticia.UpdateFonte(request.Fonte);
                noticia.UpdateImgUrl(request.ImgUrl);
                noticia.UpdateAutor(request.Autor);
                noticia = _repository.Update(noticia);
                await _uow.Commit();

                return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, NoticiaOutput.FromEntity(noticia), Enumerable.Empty<ErrorModel>());
            }
            catch (Exception)
            {
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante a atualização da notícia de título {request.Titulo}"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(DeleteNoticiaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var noticia = (await _repository.Select(id: request.Id)).FirstOrDefault();
                if (noticia == null)
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.NotFound, default(NoticiaOutput), _errors);

                _repository.Delete(noticia);
                await _uow.Commit();

                return new RequestResult<NoticiaOutput>(HttpStatusCode.NoContent, default(NoticiaOutput), _errors);
            }
            catch (Exception)
            {
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante a exclusão da notícia de ID:{request.Id}"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(InsertNoticiaIntegracaoNewsApiOrgCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var noticias = new List<Noticia>();

                var noticiasBusiness = await _newsApiOrgService.ObterNoticias(nameof(NoticiaCategoriaEnum.Business));
                if (noticiasBusiness.Any())
                    noticias.AddRange(noticiasBusiness);

                var noticiasEntertainment = await _newsApiOrgService.ObterNoticias(nameof(NoticiaCategoriaEnum.Entertainment));
                if (noticiasEntertainment.Any())
                    noticias.AddRange(noticiasEntertainment);

                var noticiasHealth = await _newsApiOrgService.ObterNoticias(nameof(NoticiaCategoriaEnum.Health));
                if (noticiasHealth.Any())
                    noticias.AddRange(noticiasHealth);

                var noticiasScience = await _newsApiOrgService.ObterNoticias(nameof(NoticiaCategoriaEnum.Science));
                if (noticiasScience.Any())
                    noticias.AddRange(noticiasScience);

                var noticiasSports = await _newsApiOrgService.ObterNoticias(nameof(NoticiaCategoriaEnum.Sports));
                if (noticiasSports.Any())
                    noticias.AddRange(noticiasSports);

                var noticiasTechnology = await _newsApiOrgService.ObterNoticias(nameof(NoticiaCategoriaEnum.Technology));
                if (noticiasTechnology.Any())
                    noticias.AddRange(noticiasTechnology);

                foreach (var noticia in noticias)
                {
                    var noticiaExistente = (await _repository.Select(titulo: noticia.Titulo)).FirstOrDefault();
                    if (noticiaExistente == null)
                        await _repository.Insert(noticia);
                }
                await _uow.Commit();

                return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            }
            catch (Exception)
            {
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante o cadastro das notícias de origem NewsApiOrg"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }
    }
}
