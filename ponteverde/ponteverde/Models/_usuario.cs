
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{    
    //LÓGICA DE NEGÓCIO
    public class UsuarioRepository : Base <usuario, object>
    {
        public UsuarioRepository(PvEntities bd)
            : base(bd)
        {

        }        

        public Tuple<usuario, bool, bool, string> ObterConta(string username, string pass)
        {
            var conta = base.Obter(x => x.username.Equals(username)).FirstOrDefault();

            bool logado = false;
            bool cliente = false;
            if(conta == null)
            {
                return new Tuple<usuario, bool, bool, string>(null, logado, cliente, "Conta inexistente. Por favor, cadastre-se!");
            }
            else
            {  
                if(conta.password.Equals(pass))
                {
                    if (conta.IsCliente)
                    {
                        logado = true;
                        cliente = true;
                    }
                    else
                    {
                        logado = true;
                    }

                    return new Tuple<usuario, bool, bool, string>(conta, logado, cliente, "Bem Vindo ao PonteVerde");
                }
                else
                {
                    return new Tuple<usuario, bool, bool, string>(conta, logado, cliente, "Senha incorreta");
                }                
            }                  
        }

        public Tuple<usuario, bool> CriarConta(usuario usuario, bool isCliente)
        {
            try
            {
                if (ExisteUsername(usuario.username))
                {
                    if (isCliente)
                    {
                        usuario.tipo = ((int)TipoUsuario.CLIENTE).ToString();
                    }
                    else
                    {
                        usuario.tipo = ((int)TipoUsuario.LOJA).ToString();
                    }

                    base.Criar(usuario);
                    base.Persistir();

                    return new Tuple<usuario,bool>(usuario, true);
                }

                return new Tuple<usuario, bool>(usuario, false);
            }
            catch
            {
                return new Tuple<usuario, bool>(usuario, false);
            }            
        }

        public bool ExisteUsername(string username)
        {
            var result = base.Obter(x => x.username == username).ToList();
            return result.Count > 0 ? false : true;  
        }
    }

    //CLASSE PARCIAL
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
        [RegularExpression(@"^(?=[A-Za-z0-9])(?!.*[._()\[\]-]{2})[A-Za-z0-9._()\[\]-]{2,30}$", ErrorMessage = "login inválido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "digite um username com 3 caracteres no mínimo!")]
        public string username { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "senha é obrigatória")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "digite uma senha de 6 caracteres no mínimo!")]
        public string password { get; set; }

        [Display(Name = "Email")]  
        [DataType(DataType.EmailAddress, ErrorMessage = "e-mail inválido.")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",ErrorMessage="e-mail inválido")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O email deve conter 2 caracteres no mínimo e 255 no máximo!")]
        public string email { get; set; }

        [Display(Name = "Tipo da Conta")]  
        public string tipo { get; set; }
      
    }

    public enum TipoUsuario
    {
        CLIENTE = 1,
        LOJA
    }



}