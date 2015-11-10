using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{


    public class ClienteRepository : Base <cliente, object>
    {
        public ClienteRepository(PvEntities bd)
            : base(bd)
        {

        }
   
        public cliente ObterPerfil(long id)
        {
            return base.Obter(id);
        }

        public cliente ObterPerfilPorConta(long idConta)
        {
            return base.Obter(x => x.idUsername.Equals(idConta)).FirstOrDefault();
        }

        public Tuple<cliente, bool, string> CriarCliente(usuario usuario, cliente cliente)
        {

            UsuarioRepository iUsuario = new UsuarioRepository(bd);
            var resultUsuario = iUsuario.CriarUsuario(usuario, true);
            if(resultUsuario.Item2)
            {
                try
                {
                    cliente.cep = "00000-000";
                    cliente.fotowall = "../Content/images/default/wall.jpg";
                    cliente.fotoperfil = "../Content/images/default/face.jpg";
                    cliente.lat = 0;
                    cliente.@long = 0;
                    cliente.idBairro = 1;
                    cliente.logradouro = "Atualizar Logradouro";
                    cliente.numero = "nº";
                    cliente.statusPublico = ((int)StatusPublicoCliente.PUBLICO).ToString();
                    cliente.idUsername = resultUsuario.Item1.id;
                    base.Criar(cliente);
                    base.Persistir();

                    return new Tuple<cliente, bool, string>(cliente, true, "Bem-vindo ao Ponte Verde");
                }
                catch (Exception)
                {
                    return new Tuple<cliente, bool, string>(cliente, false, "Desculpe, tivemos um problema.");
                }                
            }
            else
            {
                return new Tuple<cliente, bool, string>(null, false, "Esse nome de usuário já existe.");
            }
        }
    }
    
    
    [MetadataType(typeof(ClienteMetadata))]
    public partial class cliente
    {
    }

    public class ClienteMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Nome")]
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
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O endereço deve conter 2 caracteres no mínimo e 255 no máximo!")]
        public string logradouro { get; set; }
        
        [Display(Name = "Número")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "O número deve conter 2 caracteres no mínimo e 8 no máximo!")]
        public string numero { get; set; }

        [Display(Name = "Latitude")]
        public decimal lat { get; set; }

        [Display(Name = "Longitude")]
        public decimal @long { get; set; }

        [Display(Name = "Status do perfil")]
        public string statusPublico { get; set; }
    }

    public enum StatusPublicoCliente
    {
        PUBLICO = 1,
        PRIVADO        
    }
}