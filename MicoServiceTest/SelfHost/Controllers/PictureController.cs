using MicoServiceTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicoServiceTest.SelfHost.Controllers
{
    //http://localhost:8701/login
    //[RoutePrefix("api/device/v1")]
    public class PictureController : ApiController
    {
        [HttpGet, Route("add/{id}")]
        [AllowAnonymous]
       //[HttpGet]
        public IHttpActionResult addID(String id)
        {
            if (id.Equals(""))
                return NotFound();
            else
                return Ok(id);

        }
        [HttpGet, Route("remove/{id}")]
        [AllowAnonymous]
        //[HttpGet]
        public IHttpActionResult removeID(String id)
        {
            if (id.Equals(""))
                return NotFound();
            else
                return Ok(id);

        }
          [HttpGet, Route("login/")]
        public IHttpActionResult login(User user)
        {
            if (user != null)
                return Ok(user.name);
            else return NotFound();
        }
    }
}
