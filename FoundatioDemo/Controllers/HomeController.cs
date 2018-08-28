using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Foundatio.Caching;
using Foundatio.Lock;
using Foundatio.Messaging;
using Microsoft.AspNetCore.Mvc;
using FoundatioDemo.Models;

namespace FoundatioDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILockProvider _lockProvider;

        public HomeController(ILockProvider lockProvider)
        {
            _lockProvider = lockProvider;
        }

        public async Task<IActionResult> Index()
        {
            //ILockProvider locker =
            //    new ThrottlingLockProvider(new RedisHybridCacheClient(new RedisCacheClientOptions()), 1, TimeSpan.FromSeconds(90));
            var locked = await _lockProvider.IsLockedAsync("Test");

            if (!locked)
            {
                var result = await _lockProvider.AcquireAsync("Test", TimeSpan.FromSeconds(90));

                


            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
