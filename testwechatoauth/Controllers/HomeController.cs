using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using testwechatoauth.Models;

namespace testwechatoauth.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _weChatAppId;

        private readonly string _weChatAppSecret;

        public HomeController(IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            _weChatAppId = senparcWeixinSetting.Value.WeixinAppId;
            _weChatAppSecret = senparcWeixinSetting.Value.WeixinAppSecret;
        }

        public async Task<IActionResult> Index()
        {
            var tokenResult = await AccessTokenContainer.TryGetAccessTokenAsync(_weChatAppId, _weChatAppSecret);

            return Content(tokenResult);
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
