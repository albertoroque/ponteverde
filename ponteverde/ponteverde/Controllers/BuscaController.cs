using ponteverde.Helpers.ViewModel;
using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ponteverde.Controllers
{
    [RoutePrefix("Busca")]
    public class BuscaController : Controller
    {
        PvEntities bd = new PvEntities();

        // GET: Busca
        [HttpGet]
        [Route("")]
        public ActionResult Busca()
        {
            var dadosBusca = new BuscaViewModel();
            return View(dadosBusca);
        }
     
        [Route("{chave}")]
        public ActionResult BuscarNome(string chave)
        {
            var iLoja = new lojaBusinessModels(bd);
            var dadosBusca = new BuscaViewModel();

            dadosBusca.Loja = iLoja.BuscaLoja(chave).Take(30);

            var iCliente = new ClienteRepository(bd);

            dadosBusca.Cliente = iCliente.BuscaCliente(chave).Take(20);

            return PartialView("Busca",dadosBusca);
        }
    }
}