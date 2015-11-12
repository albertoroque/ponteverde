using ponteverde.Models;
using ponteverde.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ponteverde.Helpers.ViewModel
{
    public class ClienteCadastroViewModel
    {
        public usuario Usuario { get; set; }

        public cliente Cliente { get; set; }

        public LocalViewModel Local { get; set; }
    }
}