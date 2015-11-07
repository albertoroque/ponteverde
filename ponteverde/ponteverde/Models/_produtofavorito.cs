using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(ProdutoFavoritoMetadata))]
    public partial class produtofavorito
    {
    }

    public class ProdutoFavoritoMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
     
    }
}