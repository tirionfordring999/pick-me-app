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
    public class AuthController : BaseController
    {
        PickMeAppContext db;
        public AuthController(PickMeAppContext _db) : base (_db)
        {
            db = _db;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel vm)
        {
            var token = db.Users.Where(u => u.Login == vm.Login && u.Pass == vm.Pass).Select(u => u.UserId);
            return Json(new { token = token });
        }

        [HttpPost("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}