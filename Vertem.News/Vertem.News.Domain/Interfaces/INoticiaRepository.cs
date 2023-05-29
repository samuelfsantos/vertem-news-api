using Vertem.News.Domain.Entities;

namespace Vertem.News.Domain.Interfaces
{
    public interface INoticiaRepository
    {
        void Delete(Noticia noticia);
        Task<Noticia> Insert(Noticia noticia);
        Noticia Update(Noticia noticia);
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
        Task<IEnumerable<Noticia>> Select(string palavraChave);
    }
}
