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
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Digite um nome com 2 caracteres no mínimo e 45 no máximo!")]
        [Required(ErrorMessage = "Nome do produto é obrigatório")]
        public string nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Digite uma descrição com 2 caracteres no mínimo e 100 no máximo!")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string descricao { get; set; }

        [Display(Name = "Preço")]        
        [Required(ErrorMessage = "Preço é obrigatório")]
        public float preco { get; set; }

        [Display(Name = "Prioridade")]
        [Required(ErrorMessage = "Prioridade é obrigatória")]
        public decimal prioridade { get; set; }

        [Display(Name = "Data/hora de criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public System.DateTime dataCriacao { get; set; }
      
    }
}