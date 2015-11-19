using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ponteverde.Helpers.SessionController
{
    public class UserSession
    {
        //O ID DA CONTA SÓ PODE SER FEITO NO LOGIN
        public long idConta { get; set; }
        public long idCliente { get; set; }
        public long? idBairro { get; set; }
        public string meuPerfil { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public bool isCliente { get; set; }
    }
}
