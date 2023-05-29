using Vertem.News.Domain.Entities;
using Vertem.News.Domain.Outputs;
using Vertem.News.Infra.Messages;
using Vertem.News.Infra.Responses;

namespace Vertem.News.Application.Commands
{
    public class InsertNoticiaCommand : ICommand<RequestResult<NoticiaOutput>>
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Conteudo { get; private set; }
        public string Categoria { get; private set; }
        public string Fonte { get; private set; }
        public DateTime DataPublicacao { get; private set; }
        public string? ImgUrl { get; private set; }
        public string? Autor { get; private set; }


        public InsertNoticiaCommand(
            string titulo, 
            string descricao, 
            string conteudo, 
            string categoria, 
            string fonte, 
            DateTime dataPublicacao, 
            string? imgUrl, 
            string? autor)
        {
            Titulo = titulo;
            Descricao = descricao;
            Conteudo = conteudo;
            Categoria = categoria;
            Fonte = fonte;
            DataPublicacao = dataPublicacao;
            ImgUrl = imgUrl;
            Autor = autor;
        }
    }
}
