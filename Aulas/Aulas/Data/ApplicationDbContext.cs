using Aulas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aulas.Data {

   /// <summary>
   /// classe responsável pela criação e gestão da Base de Dados
   /// </summary>
   public class ApplicationDbContext : IdentityDbContext {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options) {
      }


        protected override void OnModelCreating(ModelBuilder builder) {
            /* Esta instrução importa tudo o que está pre-definido
             * na super classe
             */ 
            base.OnModelCreating(builder);

            /* Adição de dados à Base de Dados
             * Esta forma é PERSISTENTE, pelo que apenas deve ser utilizada em 
             * dados que perduram da fase de 'desenvolvimento' para a fase de 'produção'.
             * Implica efetuar um 'Add-Migration'
             * 
             * Atribuir valores às ROLES
             */
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "p", Name = "Professor", NormalizedName = "PROFESSOR" },
                new IdentityRole { Id = "adm", Name = "Administrativo", NormalizedName = "ADMINISTRATIVO" }
                );

        }

        // definição das 'tabelas'
        /// <summary>
        /// Tabela Utilizadores
        /// </summary>
        public DbSet<Utilizadores>  Utilizadores { get; set; }
      /// <summary>
      /// tabela Alunos
      /// </summary>
      public DbSet<Alunos> Alunos { get; set; }
      /// <summary>
      /// tabela Professores
      /// </summary>
      public DbSet<Professores> Professores { get; set; }
    /// <summary>
    /// Tabela Cursos
    /// </summary>
      public DbSet<Cursos> Cursos { get; set; }
   /// <summary>
   /// tabela Unidades Curriculares
   /// </summary>
      public DbSet<UnidadesCurriculares> UCs { get; set; }
    /// <summary>
    /// tabela Inscrições
    /// </summary>
      public DbSet<Inscricoes> Inscricoes { get; set; }


    }
}
