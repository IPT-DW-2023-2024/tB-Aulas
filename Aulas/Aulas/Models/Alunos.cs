using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models {

   /// <summary>
   /// Alunos é uma extensão de Utilizadores
   /// </summary>
   public class Alunos : Utilizadores {


      public Alunos() {
         ListaInscricoes = new HashSet<Inscricoes>();
      }

      /// <summary>
      /// Número atribuído ao Aluno
      /// </summary>
      public int NumAluno { get; set; }

      /// <summary>
      /// Valor a pagar pelo Aluno como propina pela frequência do Curso
      /// </summary>
      public decimal Propinas { get; set; }


      /// <summary>
      /// Data da Matrícula
      /// </summary>
      [DisplayFormat(ApplyFormatInEditMode = true, 
                     DataFormatString = "{0:dd-MM-yyyy}")]
      public DateTime DataMatricula { get; set; }

      /* ************************************************
       * Vamos criar as Relações (FKs) com outras tabelas
       * *********************************************** */

      // relacionamento do tipo N-1
      /// <summary>
      /// FK para o Curso
      /// </summary>
      [ForeignKey(nameof(Curso))] // anotação que liga CursoFK a Curso
      public int CursoFK { get; set; } // Será FK para a tabela Cursos
    /// <summary>
    /// FK para o Curso
    /// </summary>
      public Cursos Curso { get; set; }  // em rigor esta instrução seria a única necessária

      // relacionamento do tipo N-M, COM atributos do relacionamento
      // não vou referenciar a tabela 'final',
      //     mas a tabela no 'meio' do relacionamento
      // vamos representar o relacionamento N-M à custa
      //     de dois relacionamentos do tipo 1-N
      /// <summary>
      /// Lista de Inscrições de um Aluno
      /// </summary>
      public ICollection<Inscricoes> ListaInscricoes { get; set; }




   }
}
