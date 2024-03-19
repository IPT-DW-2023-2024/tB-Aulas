using System.ComponentModel.DataAnnotations;

namespace Aulas.Models {
   public class Cursos {


      public Cursos() {
         ListaUCs = new HashSet<UnidadesCurriculares>();
         ListaAlunos = new HashSet<Alunos>();
      }

      [Key]  // PK
      public int Id { get; set; }

      public string Nome { get; set; }

      public string Logotipo { get; set; }

      /* ************************************************
       * Vamos criar as Relações (FKs) com outras tabelas
       * *********************************************** */

      // relacionamento com as Unidades Curriculares
      public ICollection<UnidadesCurriculares> ListaUCs { get; set; }
      
      // relacionamento com os Alunos
      public ICollection<Alunos> ListaAlunos { get; set; }

   }
}
