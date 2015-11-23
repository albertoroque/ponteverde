using ponteverde.Helpers.SessionController;
using ponteverde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ponteverde.Controllers
{
    public class FotoController : Controller
    {
        PvEntities bd = new PvEntities();
        

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file, bool isCliente, bool isPerfil)
        {
            string path;
            Tuple<bool, string> success =  null;

            if (file != null)
            {
                //ERRO SE FOTO MAIOR QUE 3 mbytes
                if (file.ContentLength > 3200000)
                {
                    ViewBag.Image = "http://www.powertime.co.za/en/blog/wp-content/uploads/2014/03/error-mesage.png";
                    ViewBag.Error = "Imagem muito grande";
                    return PartialView();
                }

                string extension = System.IO.Path.GetExtension(file.FileName);

                if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                {
                    ViewBag.Image = "http://www.powertime.co.za/en/blog/wp-content/uploads/2014/03/error-mesage.png";
                    ViewBag.Error = "Tipo de arquivo não suportado";
                    return PartialView();
                }

                string g = Guid.NewGuid().ToString("N");

                string filename = g + extension;

                path = System.IO.Path.Combine(Server.MapPath("../Content/images/"), filename);
                string cam = ("../Content/images/" + filename);

                file.SaveAs(path);

                //ATUALIZA PERFIL COM IMAGEM 
                success = this.AtualizaPerfil(cam, isCliente, isPerfil);

                if (success.Item1)
                {
                    ViewBag.Image = cam;
                }
                else
                {
                    ViewBag.Image = "http://www.powertime.co.za/en/blog/wp-content/uploads/2014/03/error-mesage.png";
                    ViewBag.Error = "Ocorreu algum erro!";
                }
            }           
            return PartialView("~/Views/Loja/LojaConfig.cshtml");
        }


        public Tuple<bool, string> AtualizaPerfil(string cam, bool isCliente, bool isPerfil)
        {
            string partialRender;
            
            try
            {
                var session = Session["UserSession"] as UserSession;
                
                
                if (!isCliente)
                {
                    var iLoja = new lojaBusinessModels(bd);

                    var loja = iLoja.ObterPerfilPorConta(session.idConta);

                    if(isPerfil)
                    {
                        partialRender = "~/Views/Foto/FotoPerfil.cshtml";
                        loja.fotoperfil = cam;
                    }
                    else
                    {
                        partialRender = "~/Views/Foto/FotoWall.cshtml";
                        loja.fotowall = cam;
                    }

                    iLoja.Editar(loja, loja.id);
                    iLoja.Persistir();
                }
                else
                {
                    var iCliente = new clienteBusinessModels(bd);

                    var cliente = iCliente.ObterPerfilPorConta(session.idConta);

                    if (isPerfil)
                    {
                        partialRender = "~/Views/Foto/FotoPerfil.cshtml";
                        cliente.fotoperfil = cam;
                    }
                    else
                    {
                        partialRender = "~/Views/Foto/FotoWall.cshtml";
                        cliente.fotowall = cam;
                    }

                    iCliente.Editar(cliente, cliente.id);
                    iCliente.Persistir();
                }

                return new Tuple<bool,string>(true, partialRender);
            }
            catch 
            {
                return new Tuple<bool, string>(false, "");
            }
        }



	}
}