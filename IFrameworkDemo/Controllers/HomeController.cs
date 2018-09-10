using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IFramework.Event;
using IFramework.Event.Impl;
using IFramework.Message;
using IFramework.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using IFrameworkDemo.Models;

namespace IFrameworkDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly DemoDbContext _dbContext;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IDemoRepository _demoRepository;

        public HomeController(DemoDbContext dbContext, IUnitOfWork unitOfWork, IDemoRepository demoRepository)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _demoRepository = demoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blog = new Blog("test1", "test1content");
            _demoRepository.Add(blog);
            await _unitOfWork.CommitAsync();
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
