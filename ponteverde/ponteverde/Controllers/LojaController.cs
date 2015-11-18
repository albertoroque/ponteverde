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
                        return RedirectToAction("Entrar","Home", new { msg = "Agora é só logar com sua conta criada!" });
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
                    ViewBag.Image = "../../Content/images/busca.jpg";
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
            try
            {
                var session = Session["UserSession"] as UserSession;
                var iLoja = new lojaBusinessModels(bd);
                var iProduto = new ProdutoRepository(bd);

                var loja = iLoja.ObterPerfilPorConta(session.idConta);

                dadosProduto.idLoja = loja.id;
                dadosProduto.idCategoria = loja.categoria.FirstOrDefault().id;
                dadosProduto.prioridade = dadosProduto.prioridade / 100;
                iProduto.CriarProduto(dadosProduto);

                return RedirectToAction("LojaConfig");
            }
            catch (Exception ex)
            {                
                throw ex;
            }                        
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