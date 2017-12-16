namespace Item.Controllers
{
    using Item.Services;
    using Lib;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class HomeController : Controller
    {
        private readonly ILocalCacheProvider _localCache;
        private readonly IRemoteCacheProvider _remoteCache;
        private readonly IDemoService _service;

        public HomeController(ILocalCacheProvider localCache, IRemoteCacheProvider remoteCache, IDemoService service)
        {
            this._localCache = localCache;
            this._remoteCache = remoteCache;
            this._service = service;
        }

        public IActionResult Index()
        {
            TimeSpan ts = TimeSpan.FromMinutes(5);
            //ViewBag.Cache = _localCache.Get("Test", () => "123", ts).ToString();
            ViewBag.Cache = _localCache.Get("Test", () =>
            {
                return _remoteCache.Get("Test", () => _service.Get(), ts);

            }, ts).ToString();
            return View();
        }
    }
}
