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

        public void Delete(Noticia noticia)
        {
            _context.Noticias.Remove(noticia);
        }

        public async Task<Noticia> Insert(Noticia noticia)
        {
            var insertResult = await _context.Noticias.AddAsync(noticia);

            return insertResult.Entity;
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

        public Noticia Update(Noticia noticia)
        {
            var updateResult = _context.Noticias.Update(noticia);

            return updateResult.Entity;
        }
    }
}
