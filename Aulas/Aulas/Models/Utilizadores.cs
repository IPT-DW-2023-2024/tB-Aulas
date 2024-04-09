using System.ComponentModel.DataAnnotations;

namespace Aulas.Models {
   public class Utilizadores {


      [Key] // PK
      public int Id { get; set; }

      public string Nome { get; set; }

      public DateOnly DataNascimento { get; set; }

   }
}
