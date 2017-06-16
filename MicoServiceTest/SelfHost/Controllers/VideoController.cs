using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicoServiceTest.SelfHost.Controllers
{

    //[RoutePrefix("api/device/v1")]
   public  class VideoController :ApiController
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
    }
}
