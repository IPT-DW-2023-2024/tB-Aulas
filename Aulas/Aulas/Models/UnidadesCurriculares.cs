using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models {
   public class UnidadesCurriculares {
      // Vamos utilizar a Entity Framework
      // https://learn.microsoft.com/en-us/ef/


      public UnidadesCurriculares() {
         ListaProfessores = new HashSet<Professores>();
         ListaInscricoes=new HashSet<Inscricoes>();
      }


      [Key] // PK
      public int Id { get; set; }

      public string Nome { get; set; }

      public int AnoCurricular { get; set; }

      public int Semestre { get; set; }

      /* ************************************************
       * Vamos criar as Relações (FKs) com outras tabelas
       * *********************************************** */

      // relacionamento do tipo N-1
      [ForeignKey(nameof(Curso))] // anotação que liga CursoFK a Curso
      public int CursoFK { get; set; } // Será FK para a tabela Cursos
      public Cursos Curso { get; set; }  // em rigor esta instrução seria a única necessária

      // relacionamento do tipo N-M, SEM atributos do relacionamento
      public ICollection<Professores> ListaProfessores { get; set; }

        // relacionamento do tipo N-M, COM atributos do relacionamento
        // não vou referenciar a tabela 'final',
        //     mas a tabela no 'meio' do relacionamento
        // vamos representar o relacionamento N-M à custa
        //     de dois relacionamentos do tipo 1-N
        public ICollection<Inscricoes> ListaInscricoes { get; set; }
    }
}
