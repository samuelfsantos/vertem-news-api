using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vertem.News.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Criacao_inicial_banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Noticia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 800, nullable: false),
                    Conteudo = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Fonte = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ImgUrl = table.Column<string>(type: "TEXT", maxLength: 350, nullable: true),
                    Autor = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticia", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Noticia");
        }
    }
}
