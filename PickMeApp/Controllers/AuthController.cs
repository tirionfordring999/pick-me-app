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
        public IActionResult Login([FromBody]LoginViewModel vm)
        {
            var token = db.Users.Where(u => u.Login == vm.Login && u.Pass == vm.Pass).Select(u => u.UserId).FirstOrDefault();
            if (token != 0)
            {
                return Json(new { token = token });
            }
            return new UnauthorizedResult();
        }

        [HttpPost("ValidateLogin")]
        public IActionResult ValidateLogin([FromBody]ValidateViewModel vm)
        {
            var count = db.Users.Where(u => u.Login == vm.Login).Count();

            if(count < 1)
            {
                return Json(new { Valid = true });
            }

            return Json(new { Valid = false });
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterViewModel vm)
        {
            var user = new Users();
            user.Login = vm.Login;
            user.BirthDate = vm.BirthDate;
            user.Pass = vm.Password;
            user.FirstName = vm.FullName;
            user.Phone = vm.PhoneNubmer;
            db.Users.Add(user);
            db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            db.SaveChanges();
            return Json(new { success = true });
        }
    }

    public class RegisterViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullName { get; set; }
        public string PhoneNubmer { get; set; }
    }

    public class ValidateViewModel
    {
        public string Login { get; set; }
    }
}