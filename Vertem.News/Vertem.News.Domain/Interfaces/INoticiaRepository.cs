﻿using Vertem.News.Domain.Entities;

namespace Vertem.News.Domain.Interfaces
{
    public interface INoticiaRepository
    {
        Task<IEnumerable<Noticia>> Select(
                    Guid? id = null,
                    string? titulo = null,
                    string? descricao = null,
                    string? conteudo = null,
                    string? categoria = null,
                    string? fonte = null,
                    DateTime? dataPublicacao = null,
                    string? imgUrl = null,
                    string? autor = null,
                    bool includes = false);
        Task<IEnumerable<Noticia>> ObterPorPalavraChaveAsync(string palavraChave);
        Task<IEnumerable<Noticia>> ObterPorFonteAsync(string fonte);
        Task<IEnumerable<Noticia>> ObterPorCategoriaAsync(string categoria);
        Task<IEnumerable<Noticia>> ObterPorTituloAsync(string titulo);
    }
}
