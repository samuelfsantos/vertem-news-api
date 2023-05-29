using Vertem.News.Domain.Entities;

namespace Vertem.News.Domain.Interfaces
{
    public interface INewsApiOrgService
    {
        Task<List<Noticia>> ObterNoticias(string request);
    }
}
