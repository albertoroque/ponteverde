using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ponteverde.Controllers.Dto
{
    public class ClienteCadastroDto
    {
        public usuario Usuario { get; set; }

        public cliente Cliente { get; set; }

        public LocalDto Local { get; set; }
    }
}