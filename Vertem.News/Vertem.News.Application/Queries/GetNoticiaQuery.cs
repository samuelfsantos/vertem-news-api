using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Queries
{
    public class GetNoticiaQuery : IQuery<RequestResult<NoticiaOutput>>
    {
        public Guid? Id { get; private set; }
        public string? Titulo { get; private set; }
        public string? Descricao { get; private set; }
        public string? Conteudo { get; private set; }
        public string? Categoria { get; private set; }
        public string? Fonte { get; private set; }
        public DateTime? DataPublicacao { get; private set; }
        public string? ImgUrl { get; private set; }
        public string? Autor { get; private set; }

        public bool SingleData { get; private set; }

        public GetNoticiaQuery(
            Guid? id = null,
            string? titulo = null,
            string? descricao = null,
            string? conteudo = null,
            string? categoria = null,
            string? fonte = null,
            DateTime? dataPublicacao = null,
            string? imgUrl = null,
            string? autor = null,
            bool singleData = false)
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

            SingleData = singleData;
        }
    }
}
