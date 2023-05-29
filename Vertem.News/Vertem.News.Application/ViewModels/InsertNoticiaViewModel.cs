namespace Vertem.News.Application.ViewModels
{
    public class InsertNoticiaViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string Categoria { get; set; }
        public string Fonte { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? ImgUrl { get; set; }
        public string? Autor { get; set; }

        public InsertNoticiaViewModel(
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
