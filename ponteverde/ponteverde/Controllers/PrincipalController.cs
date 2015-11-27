using ponteverde.Helpers.SessionController;
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
            
            var session = Session["UserSession"] as UserSession;

            IQueryable<loja> dadosLojas = null;
            
            if (session != null)
            {
                dadosLojas = iLoja.ObterPorBairro((long)session.idBairro);
            }
            else
            {
                dadosLojas = iLoja.Obter();
            }
                        
            return View(dadosLojas);
        }

        [Route("lojas/{nomeCidade}")]
        public ActionResult Principal(string nomeCidade)
        {
            var iCidade = new CidadeRepository(bd);            
            var cidade = iCidade.ObterLojas(nomeCidade);

            if (cidade == null)
            {
                return View();
            }
            
            var _bairro = cidade.bairro.Select(model => model.loja);

            var lojas = _bairro.SelectMany(model => model.AsQueryable()).AsQueryable();
                      
            return View(lojas);
        }

        [Route("lojas/{nomeCidade}/{nomeBairro}")]
        public ActionResult Principal(string nomeCidade, string nomeBairro)
        {
            var iBairro = new BairroRepository(bd);
            var bairro = iBairro.ObterLojas(nomeCidade, nomeBairro);

            if (bairro == null)
            {
                return Redirect(("~/lojas/" + nomeCidade).ToLowerInvariant());
            }
    
            var iLoja = new lojaBusinessModels(bd);
            var dadosLojas = iLoja.ObterPorBairro(bairro.id);
            
            return View(dadosLojas);
        }
    }
}