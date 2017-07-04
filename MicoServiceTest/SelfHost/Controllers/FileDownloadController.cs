using MicoServiceTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MicoServiceTest.SelfHost.Controllers
{
    //http://localhost:8701/login
    //[RoutePrefix("api/device/v1")]
    public class FileDownloadController : ApiController
    {


        /// <summary>
        /// 从WebAPI下载文件
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("file")]
        public IHttpActionResult GetFileFromWebApi()
        {
            var browser = String.Empty;
            
            //if (HttpContext.Current.Request.UserAgent != null)
            //{
            //    browser = HttpContext.Current.Request.UserAgent.ToUpper();
            //}
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "test.png");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = File.OpenRead(filePath);
            httpResponseMessage.Content = new StreamContent(fileStream);
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(filePath)

                //FileName =
                //    browser.Contains("FIREFOX")
                //        ? Path.GetFileName(filePath)
                //        : HttpUtility.UrlEncode(Path.GetFileName(filePath))
                //FileName = HttpUtility.UrlEncode(Path.GetFileName(filePath))
            };

            return ResponseMessage(httpResponseMessage);
        }
    }
}
