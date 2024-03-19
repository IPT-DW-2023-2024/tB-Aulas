using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models {
   public class Inscricoes {

      public DateTime DataInscricao { get; set; }


      /* ************************************************
       * Vamos criar as Relações (FKs) com as
       *    tabelas UnidadesCurriculares e Alunos
       * *********************************************** */

      // FK para a tabela das Unidades Curriculares
      [ForeignKey(nameof(UC))]
      public int UCFK { get; set; }
      public UnidadesCurriculares UC { get; set; }

      // FK para a tabela dos Alunos
      [ForeignKey(nameof(Aluno))]
      public int AlunoFK { get; set; }
      public Alunos Aluno { get; set; }


   }
}
