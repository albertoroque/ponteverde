using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
     [MetadataType(typeof(CidadeMetadata))]
    public partial class cidade
    {
    }

     public class CidadeRepository : Base<cidade, object>
     {
         public CidadeRepository(PvEntities bd)
            : base(bd)
        {

        }

       
         public cidade CriarCidade(string nomecidade)
         {
             var _cidade = base.Obter(x => x.nome.Equals(nomecidade)).FirstOrDefault();

             if (_cidade != null)
             {
                 return _cidade;
             }
             else
             {
                 _cidade.nome = nomecidade;
                 _cidade.idPais = 1; //BRASIL
                 base.Criar(_cidade);
                 base.Persistir();
                 return _cidade;
             }             
         }
     }

    public class CidadeMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Cidade")]  
        public string nome { get; set; }
       
    }
}