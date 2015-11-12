using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ponteverde.Helpers.ViewModel
{
    public class BuscaViewModel
    {
        public IQueryable<loja> Loja { get; set; }

        public IQueryable<cliente> Cliente { get; set; }

        public string chave { get; set; }

        public bool loja { get; set; }


    }
}