using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace TestAPIDotNetCore.Controllers
{
    public class SMSController : Controller
    {
        IConfiguration Configurations;
        
        public SMSController(IConfiguration config) 
        {
            Configurations = config;
        }


        [HttpPost("/Add")]
        [Produces("application/json", Type = typeof(double))]
        [Consumes("application/json")]
        public IActionResult Add([FromQuery()] double A, [FromQuery()] double B)
        {
            double C=A+B;
            IActionResult objAction = CreatedAtAction("Add", C);
            return objAction;
        }
    }
}