using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ponteverde.Helpers.ViewModel
{
    public class LocalViewModel
    {
        [Required(ErrorMessage = "nome da cidade é obrigatório")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "bairro é obrigatório")]
        public string bairro { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }

    }
}