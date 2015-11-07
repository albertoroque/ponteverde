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

    public class CidadeMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Cidade")]  
        public string nome { get; set; }
       
    }
}