using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PickMeApp.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index1()
        {
            return Json(new { message = "Hello World!!" });
        }
    }
}