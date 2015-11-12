using ponteverde.Helpers.ViewModel;
using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ponteverde.Controllers
{
    [RoutePrefix("Loja")]
    public class LojaController : Controller
    {
        PvEntities bd = new PvEntities();

        [Route("Cadastro")]
        public ActionResult New()
        {
            return View();
        }

        [HttpGet]
        [Route("Perfil/{idConta:long:min(1)}")]
        public ActionResult Perfil(long idConta)
        {
            var iLoja = new lojaBusinessModels(bd);

            var loja = iLoja.ObterPerfilPorConta(idConta);
            return View(loja);
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(LojaCadastroViewModel dadosLoja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lojaBusinessModels iLoja = new lojaBusinessModels(bd);
                    var result = iLoja.CriarLoja(dadosLoja);
                    if (result.Item2)
                    {
                        return RedirectToAction("Perfil", new { idConta = result.Item1.usuario.id });
                    }
                    else
                    {
                        ViewBag.Error = result.Item3;
                    }
               }
                //mensagem do model state
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
            }
            return PartialView("New", dadosLoja);
        }
    }
}