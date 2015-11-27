using ponteverde.Helpers;
using ponteverde.Helpers.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{

    [MetadataType(typeof(ClienteMetadata))]
    public class clienteBusinessModels : Base <cliente, object>
    {
        public clienteBusinessModels(PvEntities bd)
            : base(bd)
        {

        }
   
        public cliente ObterPerfil(long id)
        {
            return base.Obter(id);
        }

        public IQueryable<cliente> BuscaCliente(string chave)
        {
            return base.Obter(x => x.nome.Contains(chave));
        }

        public IQueryable<cliente> ObterClientePorBairro(long id)
        {
            return base.Obter(x => x.idBairro.Equals(id));
        }

        public cliente ObterPerfilPorConta(long idConta)
        {
            return base.Obter(x => x.idUsername.Equals(idConta)).FirstOrDefault();
        }

        public Tuple<cliente, bool, string> CriarCliente(ClienteCadastroViewModel dadosCliente)
        {

            UsuarioRepository iUsuario = new UsuarioRepository(bd);
            BairroRepository iBairro = new BairroRepository(bd);
            var resultUsuario = iUsuario.CriarConta(dadosCliente.Usuario, true);

            cliente _cliente = dadosCliente.Cliente;

            if(resultUsuario.Item2)
            {
                try
                {
                    _cliente.idBairro = iBairro.ObterBairroCadastro(dadosCliente.Local.cidade, dadosCliente.Local.bairro);
                
                    _cliente.fotowall = "../Content/images/default/wall.jpg";
                    _cliente.fotoperfil = "../Content/images/default/face.jpg";
                    _cliente.latitude = 0;
                    _cliente.longitude = 0;                    
                    _cliente.logradouro = dadosCliente.Local.endereco;
                    _cliente.numero = "nº 1";
                    _cliente.statusPublico = ((int)StatusPublicoCliente.PUBLICO).ToString();
                    _cliente.idUsername = resultUsuario.Item1.id;

                    base.Criar(dadosCliente.Cliente);
                    base.Persistir();

                    return new Tuple<cliente, bool, string>(_cliente, true, "Bem-vindo ao Ponte Verde");
                }
                catch (Exception)
                {
                    return new Tuple<cliente, bool, string>(_cliente, false, "Desculpe, tivemos um problema.");
                }                
            }
            else
            {
                return new Tuple<cliente, bool, string>(null, false, "Esse nome de usuário já existe.");
            }
        }
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
        public decimal latitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal longitude { get; set; }

        [Display(Name = "Status do perfil")]
        public string statusPublico { get; set; }
    }

    public enum StatusPublicoCliente
    {
        PUBLICO = 1,
        PRIVADO        
    }
}