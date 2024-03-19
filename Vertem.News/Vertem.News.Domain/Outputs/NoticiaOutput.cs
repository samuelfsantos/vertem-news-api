using System.Text.Json.Serialization;
using Vertem.News.Domain.Entities;
using Vertem.News.Infra.Base;

namespace Vertem.News.Domain.Outputs
{
    public class NoticiaOutput : BaseOutput
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string Categoria { get; set; }
        public string Fonte { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? ImgUrl { get; set; }
        public string? Autor { get; set; }

        public NoticiaOutput()
        {
                
        }

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
