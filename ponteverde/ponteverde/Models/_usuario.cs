using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class usuario
    {
        public bool IsCliente
        {
            get
            {
                return this.tipo == null ? false : this.tipo.Equals(((int)TipoUsuario.CLIENTE).ToString());
            }
        }
    }

    public class UsuarioMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "LogIn é obrigatório")]
        [RegularExpression(@"^(?=[A-Za-z0-9])(?!.*[._()\[\]-]{2})[A-Za-z0-9._()\[\]-]{2,30}$", ErrorMessage = "Username inválido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Digite um username com 3 caracteres no mínimo!")]
        public string username { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Digite uma senha de 6 caracteres no mínimo!")]
        public string password { get; set; }

        [Display(Name = "Email")]  
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido.")]       
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O email deve conter 2 caracteres no mínimo e 255 no máximo!")]
        public string email { get; set; }
      
    }

    public enum TipoUsuario
    {
        CLIENTE = 1,
        LOJA
    }



}