using Vertem.News.Domain.Entities;
using Vertem.News.Infra.Base;

namespace Vertem.News.Domain.Outputs
{
    public class NoticiaOutput : BaseOutput
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Conteudo { get; private set; }
        public string Categoria { get; private set; }
        public string Fonte { get; private set; }
        public DateTime DataPublicacao { get; private set; }
        public string? ImgUrl { get; private set; }
        public string? Autor { get; private set; }


        public NoticiaOutput(
            Guid id,
            string titulo,
            string descricao,
            string conteudo,
            string categoria,
            string fonte,
            DateTime dataPublicacao,
            string? imgUrl,
            string? autor)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Conteudo = conteudo;
            Categoria = categoria;
            Fonte = fonte;
            DataPublicacao = dataPublicacao;
            ImgUrl = imgUrl;
            Autor = autor;
        }

        public static NoticiaOutput FromEntity(Noticia noticia)
        {
            return new NoticiaOutput(
                    noticia.Id,
                    noticia.Titulo,
                    noticia.Descricao,
                    noticia.Conteudo,
                    noticia.Categoria,
                    noticia.Fonte,
                    noticia.DataPublicacao,
                    noticia.ImgUrl,
                    noticia.Autor);
        }
    }
}
