
namespace Management.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Lib;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IPublishCacheProvider _cache;

        public ValuesController(IPublishCacheProvider cache)
        {
            _cache = cache;
        }
         
        [HttpGet]
        public string Get()
        {
            _cache.Update("Test", DateTime.Now.ToString(), TimeSpan.FromMinutes(1));

            return "Update Remote Cache Succeed!";
        }
    }
}
