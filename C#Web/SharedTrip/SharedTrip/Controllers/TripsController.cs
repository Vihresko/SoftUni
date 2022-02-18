namespace SharedTrip.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SharedTrip.Models;
    using SharedTrip.Services;
    public class TripsController : Controller
    {
        public readonly ITripService tripService;
        public TripsController(Request request, ITripService _tripService) : base(request)
        {
            this.tripService = _tripService;
        }

        [Authorize]
        public Response Details(string tripId)
        {
            var model = tripService.GetSingleTripDetails(tripId);
            return View(model, "/Trips/Details");
        }

        [Authorize]
        public Response All()
        {
            var model = tripService.GetAllCreatedTrips();
            var modelExtra = new { model = model, IsAuthenticated = true };
            return View(modelExtra, "/Trips/All");
        }

        [Authorize]
        public Response Add()
        {
            return View(new { IsAuthenticated = true });
        }

        [Authorize]
        [HttpPost]
        public Response Add(TripAddForm tripForm)
        {
            (bool isValid, string errors) = tripService.ValidateTripAddForm(tripForm);
            if (!isValid)
            {
                return View(new { ErrorMessage = errors }, "/Error");
            }
            tripService.CreateTrip(tripForm);
            
            return Redirect("/Trips/All");
        }

        public Response AddUserToTrip(string tripId)
        {
            (bool isPosible, string error) = tripService.AddUserToTrip(tripId, User.Id);
            if (!isPosible)
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            return Redirect("/Trips/All");
        }
    }
}
