using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(LojaMetadata))]
    public partial class loja
    {

    }

    public class LojaMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Nome da loja")]
        [StringLength(75, MinimumLength = 2, ErrorMessage = "Digite um nome com 2 caracteres no mínimo e 75 no máximo!")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string nome { get; set; }

        [Display(Name = "Wall do perfil")]
        public string fotowall { get; set; }

        [Display(Name = "Foto do perfil")]
        public string fotoperfil { get; set; }

        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Display(Name = "Nome da Avenida ou Rua")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O endereço deve conter 2 caracteres no mínimo e 255 no máximo!")]
        public string logradouro { get; set; }

        [Display(Name = "Número")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "O número deve conter 2 caracteres no mínimo e 8 no máximo!")]
        public string numero { get; set; }

        [Display(Name = "Latitude")]
        public decimal lat { get; set; }

        [Display(Name = "Longitude")]
        public decimal @long { get; set; }

        [Display(Name = "Status do perfil")]
        public int cnpj { get; set; }        
    }
}