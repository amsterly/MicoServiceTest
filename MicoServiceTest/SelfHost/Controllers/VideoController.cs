﻿using MicoServiceTest.Model;
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
    }
}
