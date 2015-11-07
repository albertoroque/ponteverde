using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(PromocaoMetadata))]
    public partial class promocao
    {
    }

    public class PromocaoMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Preço é obrigatório")]
        public float novopreco { get; set; }
    }
}