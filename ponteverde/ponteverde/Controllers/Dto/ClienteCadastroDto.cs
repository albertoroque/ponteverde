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

        [Required(ErrorMessage = "cidade é obrigatória")]
        public string nomeCidade { get; set; }

        [Required(ErrorMessage = "bairro é obrigatório")]
        public string nomeBairro { get; set; }
    }
}