using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(BairroMetadata))]
    public partial class bairro : Base <bairro, object>
    {
        //CONSTRUTOR
        public bairro(PvEntities bd) : base(bd){}

        public bairro ObterBairro(long id)
        {
            var bairro = this.Obter(this.id);
            return bairro;
        }

        public IQueryable<bairro> ObterBairroDaCidade(long idCidade)
        {
            var bairrosCidade = this.Obter(x => x.idCidade.Equals(idCidade));
            return bairrosCidade;            
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