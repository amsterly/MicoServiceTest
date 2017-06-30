using MicoServiceTest.Model;
using MicoServiceTest.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MicoServiceTest.SelfHost.Controllers
{

    //下列方法运行在线程池中

    //[RoutePrefix("api/device/v1")]
    public class ParaPassController : ApiController
    {
        //    [HttpGet, Route("off/{id}")]
        //    //[HttpGet]
        //    public IHttpActionResult offID(String id)
        //    {
        //        if (id.Equals(""))
        //            return NotFound();
        //        else
        //            return Ok<String>(id);

        //    }
        //    [HttpGet, Route("on/{id}")]
        //    //[HttpGet]
        //    public IHttpActionResult onID(String id)
        //    {
        //        if (id.Equals(""))
        //            return NotFound();
        //        else
        //            return Ok(id);

        //    }

        // 简单的GET请求
        // 请求参数要与函数匹配否则报错
        //      $.ajax({
        //        type: "get",
        //        url: "http://localhost:27221/api/Charging/GetAllChargingData",
        //        data: { id: 1, name: "Jim", bir: "1988-09-11"},
        //        success: function(data, status)
        //    {
        //        if (status == "success")
        //        {
        //                $("#div_test").html(data);
        //        }
        //    }
        //});

        [HttpGet, Route("data/")]
        public string getAllData(int id, string name, DateTime bir)
        {
            string result = "Data:" + id + " " + name + " " + bir.ToString();
            Console.WriteLine(result);
            return result;

        }

        [HttpGet, Route("data/")]
        public string getAllData2([FromUri] User user)
        {
            string result = "Data:" + user.id + " " + user.name + " " + user.bir.ToString();
            Console.WriteLine(result);
            return result;

        }

        [HttpGet, Route("data/")]
        public string getAllData3(string strQuery)
        {
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(strQuery);
            string result = "Data:" + user.id + " " + user.name + " " + user.bir.ToString();
            return result;
        }
        //[FromBody]只能传一个参数
        [HttpPost, Route("data/getAllData4")]
        public string getAllData4([FromBody]string name)
        {
            string result = "Data:" + name;
            return result;
        }
        //Post 多个请求基础类型数据的传递问题
        [HttpPost, Route("data/getAllData5")]
        public string getAllData5(dynamic obj)
        {
            //obj.X X应与JSON 字段大小写匹配
            string result = "Data:" + obj.ID + " " + obj.NAME + " " + obj.BIR.ToString();
            return result;
        }
        //Post 单个实体作为参数(非Json)
        [HttpPost, Route("data/getAllData6")]
        public string getAllData6(User user)
        {
            //obj.X X应与JSON 字段大小写匹配
            string result = "Data:" + user.id + " " + user.name + " " + user.bir.ToString();
            return result;
        }

        //Post 单个实体作为参数(Json)
        [HttpPost, Route("data/getAllData7")]
        public string getAllData7(User user)
        {
            //obj.X X应与JSON 字段大小写匹配
            string result = "Data:" + user.id + " " + user.name + " " + user.bir.ToString();
            return result;
        }

        //Post 基础和实体类 HirBird
        [HttpPost, Route("data/getAllData8")]
        public string getAllData8(dynamic obj)
        {
            //obj.X X应与JSON 字段大小写匹配
            var strName = Convert.ToString(obj.Name);
            var strUser= Newtonsoft.Json.JsonConvert.DeserializeObject<User>(Convert.ToString(obj.User));
            return strName+"   "+strUser;
        }

        //Post Arr 基础类型
        [HttpPost, Route("data/getAllData9")]
        public string getAllData9(string [] ids)
        {
            var idsq = "";
            foreach (var item in ids)
            {
                idsq += item + " ";
            }

            return idsq;
        }

        //Post Arr Bean类型
        [HttpPost, Route("data/getAllData10")]
        public string getAllData10(List<User> list)
        {
            var ids = "";
            foreach (var item in list)
            {
                ids += item.id+" ";
            }

            return ids;
        }

        [HttpPut, Route("data/Update")]
        public Boolean Update(dynamic obj)
        {
            if (obj.ID == "1")
                return true;
            else return false;
        }

        [HttpDelete, Route("data/Delete")]
        public Boolean Delete(dynamic obj)
        {
            string str= Convert.ToString(obj);
            List<User> userList =
            Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(str);
            return true;
        }
        [HttpGet, Route("data/getJsonBack")]
        public IHttpActionResult getJsonBack()
        {
            var users = new List<User>();
            users.Add(new User("1", "t", new DateTime()));
            users.Add(new User("2", "tt", new DateTime()));
            return Json<List<User>>(users);
        }

        //getMultyBasicBack
        [HttpGet, Route("data/getMultyBasicBack")]
        public IHttpActionResult getMultyBasicBack()
        {
         
            return Json<dynamic>(new { A="a",B="b"});
        }

        [HttpGet, Route("data/GetOKResult")]
        public IHttpActionResult GetOKResult(string name)
        {
            //return Ok();//只有200 访问成功
            return Ok<string>(name);//除了200 还能带一个str
        }

        [HttpGet, Route("data/GetNotFoundResult")]
        public IHttpActionResult GetNotFoundResult(string name)
        {
            return NotFound();//404
        }

        //getHttpStatusCode
        [HttpGet, Route("data/getHttpStatusCode")]
        public IHttpActionResult getHttpStatusCode(string name)
        {
            return Content<string>(HttpStatusCode.OK, "OK");
        }
        [HttpGet, Route("data/GetBadRequest")]
        public IHttpActionResult GetBadRequest(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            return Ok();
        }

        [HttpGet, Route("data/RedirectResult")]
        public IHttpActionResult RedirectResult()
        {
            Console.WriteLine("线程id:" + Thread.CurrentThread.ManagedThreadId);

            return Redirect("http://www.baidu.com");
        }

        [HttpGet, Route("data/GetPageRow")]
        public IHttpActionResult GetPageRow(int limit, int offset)
        {
            var lstRes = new List<User>();

            //实际项目中，通过后台取到集合赋值给lstRes变量。这里只是测试。
            lstRes.Add(new User("1", "Tomy", new DateTime()) { });
            lstRes.Add(new User("2", "Tomy", new DateTime()) {  });
            Console.WriteLine("线程id:"+Thread.CurrentThread.ManagedThreadId);
            var oData = new { total = lstRes.Count, rows = lstRes.Skip(offset).Take(limit).ToList() };
            return new PageResult(oData, Request);
        }
        //HttpResponseMessage 将文件流保存在StreamContent对象里面，然后输出到浏览器。在浏览器端即可将Excel输出。
        //http://www.cnblogs.com/landeanfen/p/5501487.html#_label1_4

        [HttpGet, Route("data/getException")]
        public IHttpActionResult getException()
        {
            throw new NotImplementedException("方法不被支持");//返回 501
        }

    }
}
