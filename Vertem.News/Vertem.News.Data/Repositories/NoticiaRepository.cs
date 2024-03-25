using Microsoft.EntityFrameworkCore;
using Vertem.News.Domain.Entities;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.Data.Contexts;

namespace Vertem.News.Infra.Data.Repositories
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly ApplicationDbContext _context;

        public NoticiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Noticia>> Select(
            Guid? id = null,
            string? titulo = null,
            string? descricao = null,
            string? conteudo = null,
            string? categoria = null,
            string? fonte = null,
            DateTime? dataPublicacao = null,
            string? imgUrl = null,
            string? autor = null,
            bool includes = false)
        {
            var noticias = _context.Noticias.Where(n =>
                            n.Id == (id ?? n.Id) &&
                            n.Titulo == (titulo ?? n.Titulo) &&
                            n.Descricao == (descricao ?? n.Descricao) &&
                            n.Conteudo == (conteudo ?? n.Conteudo) &&
                            n.Categoria == (categoria ?? n.Categoria) &&
                            n.Fonte == (fonte ?? n.Fonte) &&
                            n.DataPublicacao == (dataPublicacao ?? n.DataPublicacao) &&
                            n.ImgUrl == (imgUrl ?? n.ImgUrl) &&
                            n.Autor == (autor ?? n.Autor))
                .OrderByDescending(n => n.DataPublicacao);

            return await noticias.ToListAsync();
        }

        public async Task<IEnumerable<Noticia>> ObterPorPalavraChaveAsync(string palavraChave)
        {
            palavraChave = palavraChave.ToUpper();

            var query = _context.Noticias.AsNoTracking().Where(n =>
                            n.Id.ToString().ToUpper().Contains(palavraChave) ||
                            n.Titulo.ToUpper().Contains(palavraChave) ||
                            n.Descricao.ToUpper().Contains(palavraChave) ||
                            n.Conteudo.ToUpper().Contains(palavraChave) ||
                            n.Categoria.ToUpper().Contains(palavraChave) ||
                            n.Fonte.ToUpper().Contains(palavraChave) ||
                            (!String.IsNullOrWhiteSpace(n.ImgUrl) && n.ImgUrl.ToUpper().Contains(palavraChave)) ||
                            (!String.IsNullOrWhiteSpace(n.Autor) && n.Autor.ToUpper().Contains(palavraChave)))
                        .OrderByDescending(n => n.DataPublicacao);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Noticia>> ObterPorFonteAsync(string fonte)
        {
            fonte = fonte.ToUpper();

            var query = _context.Noticias.AsNoTracking().Where(n =>
                            n.Fonte.ToUpper() == fonte)
                        .OrderByDescending(n => n.DataPublicacao);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Noticia>> ObterPorCategoriaAsync(string categoria)
        {
            categoria = categoria.ToUpper();

            var query = _context.Noticias.AsNoTracking().Where(n =>
                            n.Categoria.ToUpper() == categoria)
                        .OrderByDescending(n => n.DataPublicacao);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Noticia>> ObterPorTituloAsync(string titulo)
        {
            titulo = titulo.ToUpper();

            var query = _context.Noticias.AsNoTracking().Where(n =>
                            n.Titulo.ToUpper() == titulo)
                        .OrderByDescending(n => n.DataPublicacao);

            return await query.ToListAsync();
        }
    }
}
