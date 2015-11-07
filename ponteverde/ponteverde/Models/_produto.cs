using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(ProdutoMetadata))]
    public partial class produto
    {
    }

    public class ProdutoMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Nome do Produto")]
        [StringLength(75, MinimumLength = 2, ErrorMessage = "Digite um nome com 2 caracteres no mínimo e 75 no máximo!")]
        [Required(ErrorMessage = "Nome do produto é obrigatório")]
        public string nome { get; set; }

        [Display(Name = "Descrição")]  
        public string descricao { get; set; }

        [Display(Name = "Preço")]        
        [Required(ErrorMessage = "Preço é obrigatório")]
        public float preco { get; set; }
      
    }
}