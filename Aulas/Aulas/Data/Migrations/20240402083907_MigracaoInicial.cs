using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aulas.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logotipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoCurricular = table.Column<int>(type: "int", nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false),
                    CursoFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UCs_Cursos_CursoFK",
                        column: x => x.CursoFK,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    NumAluno = table.Column<int>(type: "int", nullable: true),
                    Propinas = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DataMatricula = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CursoFK = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilizadores_Cursos_CursoFK",
                        column: x => x.CursoFK,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    UCFK = table.Column<int>(type: "int", nullable: false),
                    AlunoFK = table.Column<int>(type: "int", nullable: false),
                    DataInscricao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => new { x.UCFK, x.AlunoFK });
                    table.ForeignKey(
                        name: "FK_Inscricoes_UCs_UCFK",
                        column: x => x.UCFK,
                        principalTable: "UCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Utilizadores_AlunoFK",
                        column: x => x.AlunoFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProfessoresUnidadesCurriculares",
                columns: table => new
                {
                    ListaProfessoresId = table.Column<int>(type: "int", nullable: false),
                    ListaUCsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessoresUnidadesCurriculares", x => new { x.ListaProfessoresId, x.ListaUCsId });
                    table.ForeignKey(
                        name: "FK_ProfessoresUnidadesCurriculares_UCs_ListaUCsId",
                        column: x => x.ListaUCsId,
                        principalTable: "UCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProfessoresUnidadesCurriculares_Utilizadores_ListaProfessoresId",
                        column: x => x.ListaProfessoresId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_AlunoFK",
                table: "Inscricoes",
                column: "AlunoFK");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessoresUnidadesCurriculares_ListaUCsId",
                table: "ProfessoresUnidadesCurriculares",
                column: "ListaUCsId");

            migrationBuilder.CreateIndex(
                name: "IX_UCs_CursoFK",
                table: "UCs",
                column: "CursoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_CursoFK",
                table: "Utilizadores",
                column: "CursoFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricoes");

            migrationBuilder.DropTable(
                name: "ProfessoresUnidadesCurriculares");

            migrationBuilder.DropTable(
                name: "UCs");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
