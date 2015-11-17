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

    public class CategoriaRepository : Base<categoria, object>
    {
        public CategoriaRepository(PvEntities bd)
            : base(bd)
        {

        }

        public categoria CriarCategoria(long idLoja, string categoria)
        {
            try
            {
                var _categoria = new categoria();
                _categoria.idLoja = idLoja;
                _categoria.descricao = categoria;
                base.Criar(_categoria);
                base.Persistir();
                return _categoria;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    public class CategoriaMetadata
    {
        public long id { get; set; }
        public string descricao { get; set; }        
    }
}