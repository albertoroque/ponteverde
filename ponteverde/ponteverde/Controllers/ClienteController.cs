using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ponteverde.Models;
using ponteverde.Controllers.Dto;

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

        [Route("Cadastro")]
        public ActionResult New()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(ClienteCadastroDto dadosCliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClienteRepository iCliente = new ClienteRepository(bd);
                    var result = iCliente.CriarCliente(dadosCliente.Usuario, dadosCliente.Cliente);

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