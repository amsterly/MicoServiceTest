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

        private static  string url = "http://localhost:8701/file";
        static void Main(string[] args)
        {
            //JoinText();
            cc: Console.WriteLine("请选择下列操作(输入数字后回车)：\n 1:发送Post请求 \n 2：上传多个文件\n 3.下载文件\n 4.退出程序\n ");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num != 4)
            {
                switch (num)
                {
                    case 1: TestReques(); Console.WriteLine("已发送Post请求"); break;
                    case 2: UploadFiles(); Console.WriteLine("已上传多个文件"); break;
                    case 3: if (DownloadFile(url, Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "nb.png"))) Console.WriteLine("已下载文件"); break;

                    default: Console.WriteLine("错误的输入数字[1-4]后回车"); break;
                }
                goto cc;
            }

            Environment.Exit(0);


        }

        /*Join方法效果Demo 先执行add（6s）sub 主线程等待两个线程执行结束，sub中addthread.join（2s）等待add 2秒 因需要执行6s 返回false 
         */
        private static void JoinText()
        {
            Calculate calculate = new Calculate();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("主线程输出:准备进行加法和减法两种运算:");

            calculate.threadAdd.Start();
            calculate.threadSub.Start();
            calculate.threadAdd.Join();
            calculate.threadSub.Join();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("主线程输出:所有运算完毕");
            Console.ReadKey();
        }


        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static  bool DownloadFile(string url, string path)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Copy(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
