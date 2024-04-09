using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models {

   // Alunos é uma extensão de Utilizadores
   public class Alunos : Utilizadores {


      public Alunos() {
         ListaInscricoes = new HashSet<Inscricoes>();
      }


      public int NumAluno { get; set; }

      public decimal Propinas { get; set; }

      public DateTime DataMatricula { get; set; }

      /* ************************************************
       * Vamos criar as Relações (FKs) com outras tabelas
       * *********************************************** */

      // relacionamento do tipo N-1
      [ForeignKey(nameof(Curso))] // anotação que liga CursoFK a Curso
      public int CursoFK { get; set; } // Será FK para a tabela Cursos
      public Cursos Curso { get; set; }  // em rigor esta instrução seria a única necessária

      // relacionamento do tipo N-M, COM atributos do relacionamento
      // não vou referenciar a tabela 'final',
      //     mas a tabela no 'meio' do relacionamento
      // vamos representar o relacionamento N-M à custa
      //     de dois relacionamentos do tipo 1-N
      public ICollection<Inscricoes> ListaInscricoes { get; set; }




   }
}
