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

        [HttpGet("Points")]
        public IActionResult GetData()
        {
            var user = GetUser();
            if(user == null)
            {
                return new UnauthorizedResult();
            }
            var points = db.Points.Select(s => new { s.PointId, s.PointName }).ToList();
            return Json(new { points = points });
        }

        [HttpPost("SearchRides")]
        public IActionResult SearchRides([FromBody] SearchRidesViewModel vm)
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }
            var rides = db.Routes.Where(r =>
                r.RoutePoints.Any(srp => (srp.PointId == vm.start) &&
                (r.RoutePoints.Where(erp => erp.PointId == vm.end).FirstOrDefault().PointOrder > srp.PointOrder)))
                .Select(
                s => new
                {
                    date = s.DateOfRoute,
                    time = s.TimeOfRoute,
                    s.Driver,
                    points = s.RoutePoints.Select(rp => new { rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).OrderBy(o => o.PointOrder),
                    s.Car,
                    NumberOfSeats = s.NumberOfSeats - (s.RouteRequest.Where(rr => rr.Status == 1).Sum(l => l.NumberOfSeats)??0),
                    startPoint = s.RoutePoints.Where( sp => sp.PointId == vm.start).Select(rp => new { rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).FirstOrDefault(),
                    endPoint = s.RoutePoints.Where(ep => ep.PointId == vm.end).Select(rp => new { rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).FirstOrDefault(),
                    Price = s.RoutePoints.FirstOrDefault(ep => ep.PointId == vm.end).Price - s.RoutePoints.FirstOrDefault(sp => sp.PointId == vm.start).Price

                }).ToList();

            return Json(new { rides = rides });
        }
    }

    public class SearchRidesViewModel {

        public int start { get; set; }
        public int end { get; set; }
        public object date { get; set; }
        public object time { get; set; }

    }
}