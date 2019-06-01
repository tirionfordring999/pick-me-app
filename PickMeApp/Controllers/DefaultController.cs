using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PickMeApp.Controllers
{
    public class DefaultController : Controller
    {
        PickMeAppDbContext db;

        public DefaultController()
        {
            db = new PickMeAppDbContext();
        }

        public IActionResult Hello()
        {

            var points = db.Points.ToList();
            return Json(new { points = points });
        }
    }
}