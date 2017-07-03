﻿using MicoServiceTest.Extention;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicoServiceTest.SelfHost.Controllers
{
    //[RoutePrefix("api/test")]
    public class FileUploadController : ApiController
    {
        private static readonly string ServerUploadFolder =   Path.GetTempPath();//"C:\\Temp";

        [Route("data/files")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> UploadSingleFile()
        {
            var streamProvider = new MultipartFormDataStreamProvider(ServerUploadFolder);
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            return new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow, 
                DownloadLink = "TODO, will implement when file is persisited"
            };
        }

        [Route("data/filesNoContentType")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> UploadMultipleFiles2()
        {

            //解决保存后文件名称乱码问题
            var provider = new CustomMultipartFormDataStreamProvider(ServerUploadFolder);
            try
            {
                var streamProvider = StreamConversion();
                await streamProvider.ReadAsMultipartAsync(provider);
                return new FileResult
                {
                    FileNames = provider.FileData.Select(entry => entry.LocalFileName),
                    Names = provider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),                  
                    Description = provider.FormData["description"],
                    CreatedTimestamp = DateTime.UtcNow,

                    UpdatedTimestamp = DateTime.UtcNow,
                    DownloadLink = "TODO, will implement when file is persisited"
                };
            }
            catch (System.Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private StreamContent StreamConversion()
        {
            Stream reqStream = Request.Content.ReadAsStreamAsync().Result;
            var tempStream = new MemoryStream();
            reqStream.CopyTo(tempStream);

            tempStream.Seek(0, SeekOrigin.End);
            var writer = new StreamWriter(tempStream);
            writer.WriteLine();
            writer.Flush();
            tempStream.Position = 0;

            var streamContent = new StreamContent(tempStream);
            foreach (var header in Request.Content.Headers)
            {
                streamContent.Headers.Add(header.Key, header.Value);
            }
            return streamContent;
        }

    }


}

