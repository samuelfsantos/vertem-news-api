using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vertem.News.Domain.Entities;

namespace Vertem.News.Infra.Data.Mappings
{
    public class NoticiaMapping : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Titulo).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(800).IsRequired();
            builder.Property(p => p.Conteudo).IsRequired();
            builder.Property(p => p.Categoria).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Fonte).IsRequired().HasMaxLength(180);
            builder.Property(p => p.DataPublicacao).IsRequired();
            builder.Property(p => p.ImgUrl).HasMaxLength(350);
            builder.Property(p => p.Autor).HasMaxLength(200);

            builder.Property(p => p.CriadoEm).IsRequired();
            builder.Property(p => p.AlteradoEm);

            builder.ToTable("Noticia");
        }
    }
}
