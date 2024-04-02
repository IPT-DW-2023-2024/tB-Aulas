using Aulas.Models;

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

        // definição das 'tabelas'
        public DbSet<Utilizadores>  Utilizadores { get; set; }
        public DbSet<Alunos> Alunos { get; set; }
        public DbSet<Professores> Professores { get; set; }
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<UnidadesCurriculares> UCs { get; set; }
        public DbSet<Inscricoes> Inscricoes { get; set; }


    }
}
