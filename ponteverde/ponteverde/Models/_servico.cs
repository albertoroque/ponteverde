using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(ServicoMetadata))]
    public partial class servico
    {
    }

    public class ServicoMetadata
    {
        public long id { get; set; }

        [Display(Name = "Título")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Digite um título com 2 caracteres no mínimo e 20 no máximo!")]
        [Required(ErrorMessage = "Título é obrigatório")]
        public string titulo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Digite uma descrição com 2 caracteres no mínimo e 45 no máximo!")]
        [Required(ErrorMessage = "Descrição é obrigatório")]
        public string descricao { get; set; }
    }
}