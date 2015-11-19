using ponteverde.Helpers.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(LojaFavoritaMetadata))]
    public class lojaFavoritaBusinessModels : Base<lojafavorita, object>
    {
        public lojaFavoritaBusinessModels(PvEntities bd)
            : base(bd)
        {

        }

        public ICollection<lojafavorita> ObterPorCliente(long idConta)
        {
            clienteBusinessModels iCliente = new clienteBusinessModels(bd);
            var cliente = iCliente.ObterPerfilPorConta(idConta);
            return cliente.lojafavorita;            
        }

        public Tuple<string, bool> CriarFavorito(lojafavorita dadosFavorito)
        {
            try
            {
                var favorita = base.Obter(x => x.idCliente.Equals(dadosFavorito.idCliente) && x.idLoja.Equals(dadosFavorito.idLoja));

                if (favorita.Count() < 1)
                {
                    base.Criar(dadosFavorito);
                    base.Persistir();
                    return new Tuple<string,bool>("Loja favoritada com sucesso", true);
                }
                return new Tuple<string, bool>("Essa loja já foi favoritada por você", false);
            }
            catch 
            {
                return new Tuple<string, bool>("Ocorreu algum erro!", false);
            }
            
        }
    }

    public class LojaFavoritaMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
     
    }
}