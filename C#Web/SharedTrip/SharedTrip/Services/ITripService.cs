namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using System.Collections.Generic;

    public  interface ITripService
    {
        public (bool isValid, string errors) ValidateTripAddForm(TripAddForm tripForm);

        public bool CreateTrip(TripAddForm tripForm);
        public ICollection<TripsListingModel> GetAllCreatedTrips();

        public TripDetailsModel GetSingleTripDetails(string tripId);

        public (bool isValid, string error) AddUserToTrip(string tripId, string userId);
    }
}
