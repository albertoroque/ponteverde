using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ponteverde.Models;
using ponteverde.Helpers.SessionController;

namespace ponteverde.Controllers
{  
      
    public class HomeController : Controller
    {        
        PvEntities bd = new PvEntities();
       
        public ActionResult Index()
        {
            return View();
        }


        [Route("entrar/{msg?}")]
        public ActionResult Entrar(string msg)
        {
            ViewBag.Login = msg;
            return View();
        }
                
        public ActionResult Logar(string username, string password, string msg)
        {
            UsuarioRepository iUsuario = new UsuarioRepository(bd);            

            var usuario = iUsuario.ObterConta(username, password);

            var session = new UserSession();

            if (usuario.Item2)
            {
                if(usuario.Item3)
                {
                    clienteBusinessModels iCliente = new clienteBusinessModels(bd);
                    
                    session.idConta = usuario.Item1.id;
                    var cliente = iCliente.ObterPerfilPorConta(session.idConta);
                    session.isCliente = true;
                    session.idConta = cliente.idUsername;
                    session.meuPerfil = "Cliente/Perfil/" + cliente.idUsername;
                    session.idBairro = cliente.idBairro;
                    session.nome = cliente.nome;
                    session.email = cliente.usuario.email;          

                    Session["UserSession"] = session;
                    return RedirectToAction("Perfil", "Cliente", new { idConta = usuario.Item1.id });
                }
                else
                {
                    var iLoja = new lojaBusinessModels(bd);
                    
                    session.idConta = usuario.Item1.id;
                    session.isCliente = false;                    
                    var loja = iLoja.ObterPerfilPorConta(session.idConta);                    
                    session.idConta = loja.idUsername;
                    session.meuPerfil = "Loja/Perfil/" + loja.idUsername;
                    session.idBairro = loja.idBairro;
                    session.nome = loja.nome;
                    session.email = loja.usuario.email;

                    Session["UserSession"] = session;
                    return RedirectToAction("Perfil", "Loja", new { idConta = usuario.Item1.id });                    
                }                
            }
            else
            {
                ViewBag.Login = usuario.Item4;
                return View("Entrar");
            }            
        }
    }
}
