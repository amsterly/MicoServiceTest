using MicoServiceTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicoServiceTest.SelfHost.Controllers
{

    //[RoutePrefix("api/device/v1")]
    public class VideoController : ApiController
    {
        [HttpGet, Route("off/{id}")]
        //[HttpGet]
        public IHttpActionResult offID(String id)
        {
            if (id.Equals(""))
                return NotFound();
            else
                return Ok<String>(id);

        }
        [HttpGet, Route("on/{id}")]
        //[HttpGet]
        public IHttpActionResult onID(String id)
        {
            if (id.Equals(""))
                return NotFound();
            else
                return Ok(id);

        }

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

        [HttpPost, Route("data/getAllData5")]
        public string getAllData5(dynamic obj)
        {
            string result = Convert.ToString(obj);
            return result;
        }
    }
}
