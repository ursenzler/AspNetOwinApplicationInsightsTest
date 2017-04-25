namespace WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("test")]
    public class MyController : ApiController
    {
        public MyController()
        {
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<string> Get()
        {
            throw new Exception("intentional exception");
        }
    }
}