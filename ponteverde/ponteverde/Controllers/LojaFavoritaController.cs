using ponteverde.Helpers.SessionController;
using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ponteverde.Controllers
{
    [RoutePrefix("LojasFavoritas")]
    public class LojaFavoritaController : Controller
    {
        PvEntities bd = new PvEntities();
     
        [HttpGet]
        [Route("")]
        public ActionResult Favoritas()
        {                
            var iLojaFav = new lojaFavoritaBusinessModels(bd);
            var session = Session["UserSession"] as UserSession;
            var listaLojas = iLojaFav.ObterPorCliente(session.idConta);              
            return View(listaLojas);
        }
    }
}