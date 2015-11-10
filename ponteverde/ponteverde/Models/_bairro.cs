using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(BairroMetadata))]
    public partial class bairro
    {
       
    }

    public class BairroRepository : Base<bairro, object>
    {
        public BairroRepository(PvEntities bd)
            : base(bd)
        {

        }  
         
         public bairro ObterBairro(long id)
         {
             return base.Obter(id);              
         }
         
         public IQueryable<bairro> ObterBairroDaCidade(long idCidade)
         {
             return base.Obter(x => x.idCidade.Equals(idCidade));             
         }

         public long ObterBairro(string bairro, string cidade)
         {             
             //var bairro = base.Obter(x => (x.nome.Contains(bairro)));

             return bairro.id;

         }
    }

    public class BairroMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Nome do bairro")]  
        public string nome { get; set; }       
    }


}