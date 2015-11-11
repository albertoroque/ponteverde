using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(BairroMetadata))]
    public partial class bairro
    {
       
    }

    public class BairroRepository : Base<bairro, object>
    {
        public BairroRepository(PvEntities bd)
            : base(bd)
        {

        }  
         
         public bairro ObterBairro(long id)
         {
             return base.Obter(id);              
         }
         
         public IQueryable<bairro> ObterBairroDaCidade(long idCidade)
         {
             return base.Obter(x => x.idCidade.Equals(idCidade));             
         }

         public bairro CriarBairro(string nomeBairro, string nomeCidade)
         {
             CidadeRepository iCidade = new CidadeRepository(this.bd);
             var _cidade = iCidade.CriarCidade(nomeCidade);
             
             bairro _bairro = new bairro();
             _bairro.nome = nomeBairro;
             _bairro.idCidade = _cidade.id;
             base.Criar(_bairro);
             base.Persistir();
             return _bairro;
         }

         public long ObterBairroCadastro(string nomeBairro, string nomeCidade)
         {             
            bairro _bairro = this.Obter(x => x.nome.Equals(nomeBairro) && x.cidade.nome.Equals(nomeCidade)).FirstOrDefault();

            if (_bairro != null)
            {
                return _bairro.id;
            }
            else
            {
                var result = this.CriarBairro(nomeBairro, nomeCidade);
                return result.id;
            }
         }
    }

    public class BairroMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Display(Name = "Nome do bairro")]  
        public string nome { get; set; }       
    }


}