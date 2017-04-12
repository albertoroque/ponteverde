using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;

namespace uploader.Controllers
{

    [RoutePrefix("api/upload")]
    public class ImagemController : ApiController
    {       

        //http://stackoverflow.com/questions/10320232/how-to-accept-a-file-post-asp-net-mvc-4-webapi
        //http://www.c-sharpcorner.com/UploadFile/2b481f/uploading-a-file-in-Asp-Net-web-api/

        [HttpPost, Route("")]
        public IHttpActionResult Post()
        {

            try
            {
                object result = null;
                var httpRequest = HttpContext.Current.Request;
               
                if (httpRequest.Files.Count > 0)
                {
                    
                    foreach (string file in httpRequest.Files)
                    {                 
                        var postedFile = httpRequest.Files[file];
                 
                        string extension = System.IO.Path.GetExtension(postedFile.FileName).Trim().ToLower();
                                               
                        if (!extension.Equals(".jpg") && !extension.Equals(".png") && !extension.Equals(".jpeg"))
                        {
                            result = "Extensão não suportada"; 
                          
                        }else 
                        if(postedFile.ContentLength > 3200000)
                        {
                            result = "Tamanho de arquivo não supotado";
                        }
                        else
                        {
                            string g = Guid.NewGuid().ToString("N");

                            string filename = g + extension;
                           
                            var filePath = HttpContext.Current.Server.MapPath("~/img/" + filename);

                            postedFile.SaveAs(filePath);

                            result = "http://" + HttpContext.Current.Request.Url.Authority + "/img/" + filename;
                            return this.Ok(result); 
                        }

                        return this.BadRequest((string)result);
                    }

                    return this.InternalServerError();
                }
                else
                {
                    return this.InternalServerError();
                }                
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
                      
        }        
    }
}