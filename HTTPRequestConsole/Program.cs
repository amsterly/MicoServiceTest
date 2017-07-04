using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTTPRequestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请选择下列操作(输入数字后回车)：\n 1:发送Post请求 \n 2：上传多个文件\n 3.下载文件\n 4.退出程序\n ");
            int num = Convert.ToInt32(Console.ReadLine());
            while (num!=4)
            {
                switch (num)
                {
                    case 1: TestReques(); Console.WriteLine("已发送Post请求"); break;
                    case 2: UploadFiles(); Console.WriteLine("已上传多个文件"); break;
                    case 3: break;
             
                    default: Console.WriteLine("错误的输入数字[1-4]后回车"); break;
                }
            }

            Environment.Exit(0);


        }

        public static  void TestReques()
        {
            //请求路径
            string url = "http://localhost:8701/data/getAllData6";

            //定义request并设置request的路径
            WebRequest request = WebRequest.Create(url);
            request.Method = "post";

            //初始化request参数
            string postData = "{ ID: \"1\", NAME: \"Jim\", BIR: \"1988-09-11\" }";

            //设置参数的编码格式，解决中文乱码
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //设置request的MIME类型及内容长度
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //打开request字符流
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //定义response为前面的request响应
            WebResponse response = request.GetResponse();

            //获取相应的状态代码
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            //定义response字符流
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();//读取所有
            Console.WriteLine(responseFromServer);
        }
        public static void UploadFiles()
        {
            var message = new HttpRequestMessage();
            var content = new MultipartFormDataContent();
            var files = new List<string> { "WebApiDoc01.png", "WebApiDoc02.png" };

            foreach (var file in files)
            {
                var filestream = new FileStream(file, FileMode.Open);
                var fileName = System.IO.Path.GetFileName(file);
                content.Add(new StreamContent(filestream), "file", fileName);
            }

            message.Method = HttpMethod.Post;
            message.Content = content;
            message.RequestUri = new Uri("http://localhost:8701/data/filesNoContentType");

            var client = new HttpClient();
            client.SendAsync(message).ContinueWith(task =>
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    var result = task.Result;
                    Console.WriteLine(result);
                }
            });

            Console.ReadLine();
        }
    }
}
