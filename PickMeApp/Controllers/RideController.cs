using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickMeApp.Models;
using PickMeApp.ViewModel;

namespace PickMeApp.Controllers
{
    [Route("api/[controller]")]
    public class RideController : BaseController
    {
        PickMeAppContext db;
        public RideController(PickMeAppContext _db) : base(_db)
        {
            db = _db;
        }

        [HttpGet("GetData")]
        public IActionResult GetData()
        {
            var user = GetUser();
            if(user == null)
            {
                return new UnauthorizedResult();
            }
            return Json(new { User = user });
        }
    }
}