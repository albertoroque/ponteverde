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
    [RoutePrefix("Cliente")]
    public class ClienteController : Controller
    {
        PvEntities bd = new PvEntities();
        
        [HttpGet]
        [Route("Perfil/{idConta:long:min(1)}")]
        public ActionResult Perfil(long idConta)
        {
            clienteBusinessModels iCliente = new clienteBusinessModels(bd);

            var cliente = iCliente.ObterPerfilPorConta(idConta);
                       
            var session = Session["UserSession"] as UserSession;
            if (cliente.idUsername == session.idConta)
            {
                session.idConta = cliente.idUsername;
                session.meuPerfil = "Cliente/Perfil/" + cliente.idUsername;
                session.idBairro = cliente.idBairro;
                session.nome = cliente.nome;
                session.email = cliente.usuario.email;               
            }

            Session["UserSession"] = session;   
            
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
                        return RedirectToAction("Perfil", new { idConta = result.Item1.usuario.id });
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