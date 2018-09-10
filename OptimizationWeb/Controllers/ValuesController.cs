using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptimizationWeb.Models;

namespace OptimizationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IAccountDiscountCalculatorFactory _factory;

        public ValuesController(IAccountDiscountCalculatorFactory factory)
        {
            _factory = factory;
        }


        public decimal Get()
        {
            return _factory.GetAccountDiscountCalculator(AccountStatus.MostValuableCustomer).ApplyDiscount(100);
        }
    }
}