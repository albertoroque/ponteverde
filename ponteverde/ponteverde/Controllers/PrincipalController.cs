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

        [Route("lojas/{nomeCidade}")]
        public ActionResult Principal(string nomeCidade)
        {
            var iCidade = new CidadeRepository(bd);            
            var cidade = iCidade.ObterLojas(nomeCidade);
            IQueryable<loja> dadosloja = null;
            foreach (var i in cidade.bairro)
            {
                dadosloja = dadosloja.Concat(i.loja);
            }
            return View(dadosloja);
        }

        [Route("lojas/{nomeCidade}/{nomeBairro}")]
        public ActionResult Principal(string nomeCidade, string nomeBairro)
        {
            var iBairro = new BairroRepository(bd);
            var bairro = iBairro.ObterLojas(nomeCidade, nomeBairro);

            var iLoja = new lojaBusinessModels(bd);
            var dadosLojas = iLoja.ObterPorBairro(bairro.id);
            
            return View(dadosLojas);
        }
    }
}