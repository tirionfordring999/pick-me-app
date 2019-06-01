using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickMeApp.Models;

namespace PickMeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : BaseController
    {
        PickMeAppContext db;

        public DefaultController(PickMeAppContext _db): base(_db)
        {
            db = _db;
        }

        [HttpGet("Hello1")]

        public IActionResult Hello()
        {
            var user = GetUser();

            return Json(new { points = db.Points.FirstOrDefault(), loggedUser = user });
        }
    }
}