namespace Aulas.Models {
   public class Professores {

      public Professores() {
         ListaUCs=new HashSet<UnidadesCurriculares>();
      }


      /* ************************************************
      * Vamos criar as Relações (FKs) com outras tabelas
      * *********************************************** */

      // relacionamento do tipo N-M, SEM atributos do relacionamento
      public ICollection<UnidadesCurriculares> ListaUCs { get; set; }

   }
}
