using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ponteverde.Controllers
{
    [RoutePrefix("")]
    public class PrincipalController : Controller
    {
        PvEntities bd = new PvEntities();

        [Route("lojas")]
        public ActionResult Principal()
        {
            var iLoja = new lojaBusinessModels(bd);
            var dadosLojas = iLoja.Obter();
            return View(dadosLojas);
        }
    }
}