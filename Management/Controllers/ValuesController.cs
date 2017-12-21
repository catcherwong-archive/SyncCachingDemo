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
        public string Get(int type)
        {
            if(type == 1)
            {
                _cache.Update("Test", DateTime.Now.ToString(), TimeSpan.FromMinutes(5));
            }
            else if (type == 2)
            {
                _cache.Add("Test", DateTime.Now.ToString(), TimeSpan.FromMinutes(5),true);
            }
            else
            {
                _cache.Delete("Test");
            }

            return "Update Redis Cache And Notify Succeed!";
        }
    }
}
