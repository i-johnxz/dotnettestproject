using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagHelperDemo.Models;

namespace TagHelperDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var result = new Result
            {
                Persons = new List<Person>()
                {
                    new Person()
                    {
                        FirstName = "Autumn",
                        LastName = "Gomes",
                        Age = 11,
                        EmailAddress = "Michelle.Tellies@gmx.com"
                    },
                    new Person()
                    {
                        FirstName = "Grace",
                        LastName = "Cox",
                        Age = 26,
                        EmailAddress = "Jillian.Russell@gmx.com"
                    },
                    new Person()
                    {
                        FirstName = "Angel",
                        LastName = "Gonzalez",
                        Age = 7,
                        EmailAddress = "Emma.Getzlaff@rogers.ca"
                    }
                }
            };
            return View(result);
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
