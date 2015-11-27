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

            var iCliente = new clienteBusinessModels(bd);

            dadosBusca.Cliente = iCliente.BuscaCliente(chave).Take(20);

            if (dadosBusca.Cliente == null)
            {
                ViewBag.ClienteBuscaMsg = "Sem resultado";
            }
            if (dadosBusca.Loja == null)
            {
                ViewBag.LojaBuscaMsg = "Sem resultado";
            }
            return PartialView("Busca",dadosBusca);
        }

        [Route("local/{nomeCidade}/{nomeBairro}")]
        public ActionResult BuscarLocation(string nomeCidade, string nomeBairro)
        {
            var iLoja = new lojaBusinessModels(bd);
            var dadosBusca = new BuscaViewModel();
            var ibairro = new BairroRepository(bd);
            var iCliente = new clienteBusinessModels(bd);

            var _bairro = ibairro.ObterLojas(nomeCidade, nomeBairro);

            if (_bairro == null)
            {
                return PartialView("Busca", dadosBusca);
            }

            dadosBusca.Loja = iLoja.ObterPorBairro(_bairro.id).Take(20);
            
            dadosBusca.Cliente = iCliente.ObterClientePorBairro(_bairro.id).Take(20);

            if (dadosBusca.Cliente == null)
            {
                ViewBag.ClienteBuscaMsg = "Sem resultados";
            }
            if (dadosBusca.Loja == null)
            {
                ViewBag.LojaBuscaMsg = "Sem resultados";
            }

            return PartialView("Busca", dadosBusca);
        }
    }
}