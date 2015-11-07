using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ponteverde.Models;

namespace ponteverde.Controllers
{
    [RoutePrefix("Cliente")]
    public class ClienteController : Controller
    {
        PvEntities bd = new PvEntities();
        
        [HttpGet]
        [Route("Perfil/{idConta:long:min(1)}")]
        public ActionResult Perfil(long idConta)
        {
            ClienteRepository iCliente = new ClienteRepository(bd);

            var cliente = iCliente.ObterPerfilPorConta(idConta);

            return View(cliente);
        }
    }
}