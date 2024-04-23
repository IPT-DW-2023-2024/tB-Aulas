using System.ComponentModel.DataAnnotations;

namespace Aulas.Models {
   /// <summary>
   /// classe genérica dos Utilizadores da aplicação
   /// </summary>
   public class Utilizadores {


      [Key] // PK
      public int Id { get; set; }

      /// <summary>
      /// Nome do Utilizador
      /// </summary>
      public string Nome { get; set; }

      /// <summary>
      /// Data de Nascimento
      /// </summary>
      [DataType(DataType.Date)] // informa a View de como deve tratar este atributo
      [DisplayFormat(ApplyFormatInEditMode = true,
                     DataFormatString = "{0:dd-MM-yyyy}")]
      public DateOnly DataNascimento { get; set; }

      /// <summary>
      /// número de telemóvel do Utilizador
      /// </summary>
      [Display(Name ="Telemóvel")]
      [StringLength(9)]
      // 913456789
      // +351913456789
      // 00351913456789
      [RegularExpression("9[1236][0-9]{7}",
           ErrorMessage ="o {0} só aceita 9 digitos")]
      public string Telemovel { get; set; }

   }
}
