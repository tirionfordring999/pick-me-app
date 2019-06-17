using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Where(r => r.RouteRequest.Count( rr => rr.UserId == user.UserId) == 0)
                .Select(
                s => new
                {
                    RouteId = s.RouteId,
                    date = s.DateOfRoute,
                    time = s.TimeOfRoute,
                    s.Driver,
                    points = s.RoutePoints.Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).OrderBy(o => o.PointOrder),
                    s.Car,
                    NumberOfSeats = s.NumberOfSeats,
                    SeatsBooked = (s.RouteRequest.Where(rr => rr.Status == 1).Sum(l => l.NumberOfSeats) ?? 0),
                    startPoint = s.RoutePoints.Where( sp => sp.PointId == vm.start).Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).FirstOrDefault(),
                    endPoint = s.RoutePoints.Where(ep => ep.PointId == vm.end).Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).FirstOrDefault(),
                    Price = s.RoutePoints.FirstOrDefault(ep => ep.PointId == vm.end).Price - s.RoutePoints.FirstOrDefault(sp => sp.PointId == vm.start).Price

                })
                .Where(r => r.SeatsBooked < r.NumberOfSeats).ToList();

            return Json(new { rides = rides });
        }

        [HttpPost("GetRideForBook")]
        public IActionResult GetRideForBook([FromBody] RideBookViewModel vm)
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }
            var ride = db.Routes.Where(r => r.RouteId == vm.id)
                .Select(
                s => new
                {
                    routeId = s.RouteId,
                    date = s.DateOfRoute,
                    time = s.TimeOfRoute,
                    s.Driver,
                    points = s.RoutePoints.Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).OrderBy(o => o.PointOrder),
                    s.Car,
                    NumberOfSeats = s.NumberOfSeats,
                    SeatsBooked = (s.RouteRequest.Where(rr => rr.Status == 1).Sum(l => l.NumberOfSeats) ?? 0),
                    startPoint = s.RoutePoints.Where(sp => sp.PointId == vm.start).Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).FirstOrDefault(),
                    endPoint = s.RoutePoints.Where(ep => ep.PointId == vm.end).Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).FirstOrDefault(),
                    Price = s.RoutePoints.FirstOrDefault(ep => ep.PointId == vm.end).Price - s.RoutePoints.FirstOrDefault(sp => sp.PointId == vm.start).Price

                }).FirstOrDefault();

            return Json(new { ride = ride });
        }

        [HttpPost("BookRide")]
        public IActionResult BookRide([FromBody] BookViewModel vm)
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var request = new RouteRequest()
            {
                UserId = user.UserId,
                NumberOfSeats = vm.seats,
                RouteId = vm.id,
                StartPoint = db.RoutePoints.FirstOrDefault(rp => rp.Route.RouteId == vm.id && rp.PointId == vm.start).RoutePointId,
                EndPoint = db.RoutePoints.FirstOrDefault(rp => rp.Route.RouteId == vm.id && rp.PointId == vm.end).RoutePointId,
                Status = 0
            };
            db.RouteRequest.Add(request);
            db.Entry(request).State = EntityState.Added;
            db.SaveChanges();

            return Json(new { message = "Success" });
        }


        [HttpPost("SaveRide")]
        public IActionResult SaveRide([FromBody] SaveRideViewModel vm)
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var route = new Routes();
            route.DriverId = user.UserId;
            route.DateOfRoute = vm.date;
            route.TimeOfRoute = new TimeSpan(vm.time.Value.Hour, vm.time.Value.Minute, vm.time.Value.Second);
            route.NumberOfSeats = vm.seats;
            db.Routes.Add(route);
            db.Entry(route).State = EntityState.Added;
            db.SaveChanges();

            var routePoints = new List<RoutePoints>();

            var order = 0;
            foreach(var routePoint in vm.points)
            {
                var newPoint = new RoutePoints();
                newPoint.PointId = routePoint.point.pointId;
                newPoint.RouteId = db.Entry(route).Entity.RouteId;
                newPoint.Price = routePoint.price;
                newPoint.PointOrder = order;
                order++;
                db.RoutePoints.Add(newPoint);
                db.Entry(newPoint).State = EntityState.Added;
                db.SaveChanges();
            }
            
            return Json(new { message = "Success" });
        }

        [HttpGet("GetRequests")]
        public IActionResult GetRequests()
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var requests = db.RouteRequest.Where(w => w.UserId == user.UserId).Select(s => new {
                    requestId = s.RequestId,
                    date = s.Route.DateOfRoute,
                    time = s.Route.TimeOfRoute,
                    driver = s.Route.Driver,
                    points = s.Route.RoutePoints.Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).OrderBy(o => o.PointOrder),
                    car = s.Route.Car,
                    numberOfSeats = s.NumberOfSeats,
                    startPoint = s.StartPointNavigation.Point,
                    endPoint = s.EndPointNavigation.Point,
                    price = s.EndPointNavigation.Price - s.StartPointNavigation.Price,
                    notes = s.Notes,
                    status = s.Status,
               });

            return Json(new { requests });
        }

        [HttpGet("GetRides")]
        public IActionResult GetRides()
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var routes = db.Routes.Where(w => w.DriverId == user.UserId).Select(s => new {
                routeId = s.RouteId,
                date = s.DateOfRoute,
                time = s.TimeOfRoute,
                driver = s.Driver,
                points = s.RoutePoints.Select(rp => new { rp.PointId, rp.Point.PointName, rp.RoutePointId, rp.PointOrder, rp.Price }).OrderBy(o => o.PointOrder),
                car = s.Car,
                numberOfSeats = s.NumberOfSeats,
                requests = s.RouteRequest.Select(rr => new {
                    user = rr.User,
                    requestId = rr.RequestId,
                    car = rr.Route.Car,
                    numberOfSeats = rr.NumberOfSeats,
                    startPoint = rr.StartPointNavigation.Point,
                    endPoint = rr.EndPointNavigation.Point,
                    price = rr.EndPointNavigation.Price - rr.StartPointNavigation.Price,
                    notes = rr.Notes,
                    status = rr.Status
                })
            });

            return Json(new { routes });
        }

        [HttpPost("AcceptRequest")]
        public IActionResult AcceptRequest([FromBody] AcceptRequestViewModel vm)
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var request = db.RouteRequest.FirstOrDefault(rr => rr.RequestId == vm.RequestId);
            request.Status = 1;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { message = "Success" });
        }

        [HttpPost("CancelRequest")]
        public IActionResult CancelRequest([FromBody] CancelRequestViewModel vm)
        {
            var user = GetUser();
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var request = db.RouteRequest.FirstOrDefault(rr => rr.RequestId == vm.RequestId);
            db.Entry(request).State = EntityState.Deleted;
            db.SaveChanges();

            return Json(new { message = "Success" });
        }

    }


    public class BookViewModel
    {
        public int seats { get; set; }
        public int id { get; set; }
        public int start { get; set; }
        public int end { get; set; }

    }

    public class RideBookViewModel
    {
        public int start { get; set; }
        public int end { get; set; }
        public int id { get; set; }
    }

    public class AcceptRequestViewModel
    {
        public int RequestId { get; set; }
    }

    public class CancelRequestViewModel
    {
        public int RequestId { get; set; }
    }

    public class SaveRideViewModel
    {
        public DateTime? date { get; set; }
        public DateTime? time { get; set; } 
        public List<RoutePointsViewModel> points { get; set; }
        public int seats { get; set; }
    }

    public class RoutePointsViewModel
    {
        public PointsViewModel point { get; set; }
        public int price { get; set; }
    }

    public class PointsViewModel
    {
        public int pointId { get; set; }
    }

    public class SearchRidesViewModel {

        public int start { get; set; }
        public int end { get; set; }
        public object date { get; set; }
        public object time { get; set; }

    }

}