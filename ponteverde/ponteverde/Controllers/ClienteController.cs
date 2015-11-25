using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ponteverde.Models;
using ponteverde.Helpers;
using ponteverde.Helpers.ViewModel;
using ponteverde.Helpers.SessionController;

namespace ponteverde.Controllers
{
    [RoutePrefix("cliente")]
    public class ClienteController : Controller
    {
        PvEntities bd = new PvEntities();
        
        [HttpGet]
        [Route("perfil/{idConta:long:min(1)}")]
        public ActionResult Perfil(long idConta)
        {
            clienteBusinessModels iCliente = new clienteBusinessModels(bd);
            var cliente = iCliente.ObterPerfilPorConta(idConta);

            ViewBag.ImageWall = "../" + cliente.fotowall;
            ViewBag.ImagePerfil = "../" + cliente.fotoperfil;
                     
            return View(cliente);
        }

        [Route("Cadastro")]
        public ActionResult New()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(ClienteCadastroViewModel dadosCliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clienteBusinessModels iCliente = new clienteBusinessModels(bd);
                    var result = iCliente.CriarCliente(dadosCliente);

                    if (result.Item2)
                    {
                        return RedirectToAction("Entrar","Home", new { msg = "Agora é só entrar com a sua nova conta" });
                    }
                    else
                    {
                        ViewBag.Error = result.Item3;
                    }
                }
                
            }   
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex;
            }
            
            return PartialView("New", dadosCliente);
        }
    
    }
}