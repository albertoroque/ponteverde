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
        public ActionResult FileUpload(HttpPostedFileBase file, bool isCliente, bool isPerfil, long idConta = 0)
        {
            string path;
            bool success;

            if (file != null)
            {
                //ERRO SE FOTO MAIOR QUE 3 mbytes
                if (file.ContentLength > 3200000)
                {                    
                    ViewBag.Error = "Imagem muito grande";
                    if (isCliente)
                    {
                        return RedirectToAction("Perfil", "Cliente", new { idConta = idConta });
                    }
                    return RedirectToAction("LojaConfig", "Loja");
                }

                //VERIFICAR TIPO DO ARQUIVO
                string extension = System.IO.Path.GetExtension(file.FileName);

                if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                {                 
                    ViewBag.Error = "Tipo de arquivo não suportado";
                    if (isCliente)
                    {
                        return RedirectToAction("Perfil", "Cliente", new { idConta = idConta });
                    }
                    return RedirectToAction("LojaConfig", "Loja");
                }

                //MONTA ARQUIVO NO SERVIDOR
                string g = Guid.NewGuid().ToString("N");

                string filename = g + extension;

                path = System.IO.Path.Combine(Server.MapPath("../Content/images/"), filename);
                string cam = ("../Content/images/" + filename);

                file.SaveAs(path);

                //ATUALIZA PERFIL COM IMAGEM 
                success = this.AtualizaPerfil(cam, isCliente, isPerfil);

                if (success)
                {
                    ViewBag.Image = cam;
                }
                else
                {
                    ViewBag.Image = "http://www.powertime.co.za/en/blog/wp-content/uploads/2014/03/error-mesage.png";
                    ViewBag.Error = "Ocorreu algum erro!";
                }
            }
            if (isCliente)
            {
                return RedirectToAction("Perfil", "Cliente", new { idConta = idConta });
            }            
            return RedirectToAction("LojaConfig", "Loja");
        }


        public bool AtualizaPerfil(string cam, bool isCliente, bool isPerfil)
        {                      
            try
            {
                var session = Session["UserSession"] as UserSession;
                
                
                if (!isCliente)
                {
                    var iLoja = new lojaBusinessModels(bd);

                    var loja = iLoja.ObterPerfilPorConta(session.idConta);

                    if(isPerfil)
                    {        
                        loja.fotoperfil = cam;
                    }
                    else
                    {                     
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
                        cliente.fotoperfil = cam;
                    }
                    else
                    {                     
                        cliente.fotowall = cam;
                    }

                    iCliente.Editar(cliente, cliente.id);
                    iCliente.Persistir();
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public ActionResult ReloadCliente()
        {
            var session = Session["UserSession"] as UserSession;

            return RedirectToAction("Perfil", "Cliente", new { idConta = session.idConta });
        }



	}
}