using ponteverde.Helpers.SessionController;
using ponteverde.Helpers.ViewModel;
using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ponteverde.Controllers
{
    [RoutePrefix("loja")]
    public class LojaController : Controller
    {
        PvEntities bd = new PvEntities();

        [Route("cadastro")]
        public ActionResult New()
        {
            return View();
        }

        [HttpGet]
        [Route("perfil/{idConta:long:min(1)}")]
        public ActionResult Perfil(long idConta)
        {
            var iLoja = new lojaBusinessModels(bd);
            
            var loja = iLoja.ObterPerfilPorConta(idConta);

            var session = Session["UserSession"] as UserSession;
            if (loja.idUsername == session.idConta)
            {
                session.idConta = loja.idUsername;
                session.meuPerfil = "Loja/Perfil/" + loja.idUsername;
                session.idBairro = loja.idBairro;
                session.nome = loja.nome;
                session.email = loja.usuario.email;
            }
            
            return View(loja);
        }

        [HttpPost]
        [Route("create")]
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

        [Route("config")]
        public ActionResult LojaConfig()
        {
            var session = Session["UserSession"] as UserSession;
            if (session.isCliente)
            {
                ViewBag.ErrorMsg = "Você não tem acesso a essa página.";
                return View("Erro");
            }
            else
            {
                var iLoja = new lojaBusinessModels(bd);
                var loja = iLoja.ObterPerfilPorConta(session.idConta);
                return View(loja);
            }            
        }

        [Route("config/produtos")]
        public ActionResult NewProduto(string cam)
        {
            var session = Session["UserSession"] as UserSession;
            if (session.isCliente)
            {
                ViewBag.ErrorMsg = "Você não tem acesso a essa página.";
                return View("Erro");
            }
            else
            {
                if (cam == null)
                {
                    ViewBag.Image = "http://www.datastax.com/wp-content/plugins/all-in-one-seo-pack/images/default-user-image.png";
                }
                else
                {
                    ViewBag.Image = cam;
                }
                
                return View();
            }             
        }

        [HttpPost]
        [Route("config/produtos/criar")]
        public ActionResult CreateProduto(produto dadosProduto)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            string cam = null;            
            if (file != null)
            {
                //ERRO SE FOTO MAIOR QUE 3 mbytes
                if (file.ContentLength > 3200000)
                {
                    ViewBag.Image = "http://www.powertime.co.za/en/blog/wp-content/uploads/2014/03/error-mesage.png";
                    ViewBag.Error = "Imagem muito grande";
                    return PartialView("~/Views/Loja/NewProduto.cshtml");
                }

                string extension = System.IO.Path.GetExtension(file.FileName);

                if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                {
                    ViewBag.Image = "http://www.powertime.co.za/en/blog/wp-content/uploads/2014/03/error-mesage.png";
                    ViewBag.Error = "Tipo de arquivo não suportado";
                    return PartialView("~/Views/Loja/NewProduto.cshtml");
                }

                string g = Guid.NewGuid().ToString("N");

                string filename = g + extension;
                string path = System.IO.Path.Combine(Server.MapPath("../Content/images/prod"), filename);
                cam = ("../Content/images/prod/" + filename);

                file.SaveAs(path);
            }
            ViewBag.Image = cam;
            return PartialView("~/Views/Loja/NewProduto.cshtml");
        }

    }
}