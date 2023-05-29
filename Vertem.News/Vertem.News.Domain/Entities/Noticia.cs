using Vertem.News.Infra.Base;

namespace Vertem.News.Domain.Entities
{
    public class Noticia : Entity
    {
        public string Titulo { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public string Conteudo { get; private set; } = string.Empty;
        public string Categoria { get; private set; } = string.Empty;
        public string Fonte { get; private set; } = string.Empty;
        public DateTime DataPublicacao { get; private set; }
        public string? ImgUrl { get; private set; } = null;
        public string? Autor { get; private set; } = null;


        public Noticia(
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

        public Noticia(
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


        public void UpdateTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Parâmetro [titulo] deve ser válido.", nameof(titulo));

            Titulo = titulo;
            AlteradoEm = DateTime.Now;
        }

        public void UpdateDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Parâmetro [descricao] deve ser válido.", nameof(descricao));

            Descricao = descricao;
            AlteradoEm = DateTime.Now;
        }

        public void UpdateConteudo(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
                throw new ArgumentException("Parâmetro [conteudo] deve ser válido.", nameof(conteudo));

            Conteudo = conteudo;
            AlteradoEm = DateTime.Now;
        }

        public void UpdateCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new ArgumentException("Parâmetro [categoria] deve ser válido.", nameof(categoria));

            Categoria = categoria;
            AlteradoEm = DateTime.Now;
        }

        public void UpdateFonte(string fonte)
        {
            if (string.IsNullOrWhiteSpace(fonte))
                throw new ArgumentException("Parâmetro [fonte] deve ser válido.", nameof(fonte));

            Fonte = fonte;
            AlteradoEm = DateTime.Now;
        }

        public void UpdateImgUrl(string? imgUrl)
        {
            ImgUrl = imgUrl;
            AlteradoEm = DateTime.Now;
        }

        public void UpdateAutor(string? autor)
        {
            Autor = autor;
            AlteradoEm = DateTime.Now;
        }
    }
}
