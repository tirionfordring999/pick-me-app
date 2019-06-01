using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickMeApp.Models;

namespace PickMeApp.Controllers
{
    public class DefaultController : Controller
    {
        PickMeAppContext db;

        public DefaultController()
        {
            db = new PickMeAppContext();
        }

        public IActionResult Hello()
        {


            return Json(new { points = db.Points.ToList() });
        }
    }
}