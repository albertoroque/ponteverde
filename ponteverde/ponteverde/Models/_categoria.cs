using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ponteverde.Models
{
    [MetadataType(typeof(CategoriaMetadata))]
    public partial class categoria
    {
    }

    public class CategoriaMetadata
    {
        public long id { get; set; }
        public string descricao { get; set; }        
    }
}