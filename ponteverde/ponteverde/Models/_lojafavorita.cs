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
    }

    public class LojaFavoritaMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
     
    }
}