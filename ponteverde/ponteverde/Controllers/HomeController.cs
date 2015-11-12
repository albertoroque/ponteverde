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
                
        public ActionResult Logar(string username, string password)
        {
            UsuarioRepository iUsuario = new UsuarioRepository(bd);            

            var usuario = iUsuario.ObterConta(username, password);

            var session = new UserSession();

            if (usuario.Item2)
            {
                if(usuario.Item3)
                {
                    session.id = usuario.Item1.id;
                   
                    Session["UserSession"] = session;                    
                    return RedirectToAction("Perfil", "Cliente", new { idConta = usuario.Item1.id });
                }
                else
                {
                    Session["UserSession"] = usuario.Item1.id;
                    return HttpNotFound();
                }                
            }
            else
            {
                ViewBag.Login = usuario.Item4;
                return View("Index");
            }            
        }
    }
}
