using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ponteverde.Helpers.SessionController
{
    public class UserSession
    {
        public long id { get; set; }
        public long idBairro { get; set; }

        public TipoUsuario tipo { get; set; }
    }
}