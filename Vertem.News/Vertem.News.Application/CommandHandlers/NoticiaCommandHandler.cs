using MediatR;
using Vertem.News.Application.Commands;
using Vertem.News.Domain.Entities;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Responses;
using System.Net;
using Vertem.News.Domain.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Vertem.News.Application.CommandHandlers
{
    public class NoticiaCommandHandler : IRequestHandler<InsertNoticiaCommand, RequestResult<NoticiaOutput>>,
                                        IRequestHandler<UpdateNoticiaCommand, RequestResult<NoticiaOutput>>,
                                        IRequestHandler<DeleteNoticiaCommand, RequestResult<NoticiaOutput>>,
                                        IRequestHandler<InsertNoticiaIntegracaoNewsApiOrgCommand, RequestResult<NoticiaOutput>>
    {
        private readonly List<ErrorModel> _errors;
        private readonly IUnitOfWork _uow;
        private readonly INewsApiOrgService _newsApiOrgService;
        private readonly IDistributedCache _cache;
        private readonly IGenericRepository<Noticia> _repository;
        private readonly INoticiaRepository _noticiaRepository;
        private readonly ILogger<NoticiaCommandHandler> _logger;

        public NoticiaCommandHandler(
            IUnitOfWork uow, 
            INewsApiOrgService newsApiOrgService, 
            IDistributedCache cache, 
            IGenericRepository<Noticia> repository,
            INoticiaRepository noticiaRepository,
            ILogger<NoticiaCommandHandler> logger)
        {
            _errors = new List<ErrorModel>();
            _uow = uow;
            _newsApiOrgService = newsApiOrgService;
            _cache = cache;
            _repository = repository;
            _noticiaRepository = noticiaRepository;
            _logger = logger;
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

                noticia = _repository.Adicionar(noticia);
                await _uow.Commit();

                await _cache.RemoveAsync("ObterTodasNoticias");
                await _cache.RemoveAsync("ObterNoticiasPorCategoria");
                await _cache.RemoveAsync("ObterNoticiasPorPalavraChave");
                await _cache.RemoveAsync("ObterNoticiasPorFonte");

                return new RequestResult<NoticiaOutput>(HttpStatusCode.Created, NoticiaOutput.FromEntity(noticia), Enumerable.Empty<ErrorModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar processar o comando 'InsertNoticiaCommand' | Horário: {DateTime.Now}  | Descrição do erro: {ex.Message}");
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante o cadastro da notícia de título {request.Titulo}"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(UpdateNoticiaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var noticia = await _repository.ObterAsync(request.Id);
                if (noticia == null)
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.NotFound, default(NoticiaOutput), _errors);

                noticia.UpdateTitulo(request.Titulo);
                noticia.UpdateDescricao(request.Descricao);
                noticia.UpdateConteudo(request.Conteudo);
                noticia.UpdateCategoria(request.Categoria);
                noticia.UpdateFonte(request.Fonte);
                noticia.UpdateImgUrl(request.ImgUrl);
                noticia.UpdateAutor(request.Autor);

                noticia = _repository.Modificar(noticia);
                await _uow.Commit();

                await _cache.RemoveAsync($"ObterNoticias-{request.Id}");

                return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, NoticiaOutput.FromEntity(noticia), Enumerable.Empty<ErrorModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar processar o comando 'UpdateNoticiaCommand' | Horário: {DateTime.Now}  | Descrição do erro: {ex.Message}");
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante a atualização da notícia de título {request.Titulo}"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(DeleteNoticiaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var noticia = await _repository.ObterAsync(request.Id);
                if (noticia == null)
                    return new RequestResult<NoticiaOutput>(HttpStatusCode.NotFound, default(NoticiaOutput), _errors);

                _repository.Remover(noticia);
                await _uow.Commit();

                await _cache.RemoveAsync($"ObterNoticias-{request.Id}");

                return new RequestResult<NoticiaOutput>(HttpStatusCode.NoContent, default(NoticiaOutput), _errors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar processar o comando 'DeleteNoticiaCommand' | Horário: { DateTime.Now }  | Descrição do erro: { ex.Message }");
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante a exclusão da notícia de ID:{request.Id}"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }

        public async Task<RequestResult<NoticiaOutput>> Handle(InsertNoticiaIntegracaoNewsApiOrgCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Início do processamento do comando 'InsertNoticiaIntegracaoNewsApiOrgCommand' | Horário: { DateTime.Now }");

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
                    var noticiaExistente = await _noticiaRepository.ObterPorTituloAsync(noticia.Titulo);
                    if (noticiaExistente == null)
                        _repository.Adicionar(noticia);
                }

                await _uow.Commit();

                await _cache.RemoveAsync("ObterTodasNoticias");
                await _cache.RemoveAsync("ObterNoticiasPorCategoria");
                await _cache.RemoveAsync("ObterNoticiasPorPalavraChave");
                await _cache.RemoveAsync("ObterNoticiasPorFonte");

                _logger.LogInformation($"Término do processamento do comando 'InsertNoticiaIntegracaoNewsApiOrgCommand' | Horário: { DateTime.Now }");

                return new RequestResult<NoticiaOutput>(HttpStatusCode.OK, default(NoticiaOutput), Enumerable.Empty<ErrorModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar processar o comando 'InsertNoticiaIntegracaoNewsApiOrgCommand' | Horário: { DateTime.Now } | Descrição do erro: { ex.Message }");
                _errors.Add(new ErrorModel("InternalError", $"Ocorreu um erro inesperado durante o cadastro das notícias de origem NewsApiOrg"));

                return new RequestResult<NoticiaOutput>(HttpStatusCode.InternalServerError, default(NoticiaOutput), _errors);
            }
        }
    }
}
