using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aulas.Models {

   [PrimaryKey(nameof(UCFK),nameof(AlunoFK))] // PK composta, na EF > 6.0
   public class Inscricoes {

      public DateTime DataInscricao { get; set; }


      /* ************************************************
       * Vamos criar as Relações (FKs) com as
       *    tabelas UnidadesCurriculares e Alunos
       * *********************************************** */

      // FK para a tabela das Unidades Curriculares
      [ForeignKey(nameof(UC))]
   //   [Key] // PK composta, na EF <= 6.0
      public int UCFK { get; set; }
      public UnidadesCurriculares UC { get; set; }

      // FK para a tabela dos Alunos
      [ForeignKey(nameof(Aluno))]
      //  [Key] // PK composta, na EF <= 6.0
      public int AlunoFK { get; set; }
      public Alunos Aluno { get; set; }


   }
}
