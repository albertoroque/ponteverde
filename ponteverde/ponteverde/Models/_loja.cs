﻿using ponteverde.Helpers.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(LojaMetadata))]
    public partial class lojaBusinessModels : Base<loja, object>
    {
        public lojaBusinessModels(PvEntities bd)
            : base(bd)
        {

        }

        public IQueryable<loja> ObterPorBairro(long idBairro)
        {
            var lojas = base.Obter(x => x.idBairro.Equals(idBairro));

            if (lojas == null)
                return null;

            return lojas;
        }

        public loja ObterPerfilPorConta(long idConta)
        {
            return base.Obter(x => x.idUsername.Equals(idConta)).FirstOrDefault();
        }

        public IQueryable<loja> BuscaLoja(string chave)
        {
            return base.Obter(x => x.nome.Contains(chave));
        }

        public Tuple<loja, bool, string> CriarLoja(LojaCadastroViewModel dadosLoja)
        {
            var iUsuario = new UsuarioRepository(bd);
            var iBairro = new BairroRepository(bd);
            var iCategoria = new CategoriaRepository(bd);

            var resultUsuario = iUsuario.CriarConta(dadosLoja.Usuario, false);
            loja oLoja = dadosLoja.Loja;

            if (resultUsuario.Item2)
            {
                try
                {
                    oLoja.idBairro = iBairro.ObterBairroCadastro(dadosLoja.Local.cidade, dadosLoja.Local.bairro);
                    oLoja.fotowall = "../Content/images/default/wall.jpg";
                    oLoja.fotoperfil = "../Content/images/default/face.jpg";
                    oLoja.latitude = 0;
                    oLoja.longitude = 0;
                    oLoja.logradouro = dadosLoja.Local.endereco;
                    oLoja.numero = "Nº 1";
                    oLoja.idUsername = resultUsuario.Item1.id;

                    base.Criar(oLoja);
                    base.Persistir();

                    //Criar Categoria Padrão
                    iCategoria.CriarCategoria(oLoja.id, "Todos");                    

                    return new Tuple<loja, bool, string>(oLoja, true, "Bem-vindo ao Ponte Verde!");
                }
                catch (Exception)
                {
                    return new Tuple<loja, bool, string>(oLoja, false, "Desculpe o transtorno, ocorreu algum erro!");
                }
            }
            else
            {
                return new Tuple<loja, bool, string>(null, false, "Desculpe o transtorno, ocorreu algum erro!"); 
            }

        }
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
        public decimal latitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal longitude { get; set; }

        [Display(Name = "CNPJ")]
        [StringLength(14, MinimumLength = 0, ErrorMessage = "CNPJ tem no máximo 14 caracteres!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage="Informe apenas números!")]
        public int? cnpj { get; set; }

        [Display(Name="Telefone")]
        public string telefone { get; set; }

        [Display(Name = "Link do facebook")]
        public string linkface { get; set; }
    }
}