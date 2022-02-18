namespace SharedTrip.Services
{
    using SharedTrip.Data;
    using SharedTrip.Data.DbModels;
    using SharedTrip.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using static Constrains.Constant;
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext data;
        public TripService(ApplicationDbContext dbContext)
        {
            this.data = dbContext;
        }

        public bool CreateTrip(TripAddForm tripForm)
        {
            try
            {
                var checkDateTime = DateTime.TryParseExact(tripForm.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureTime);
                Trip newTrip = new()
                {
                    StartPoint = tripForm.StartPoint,
                    EndPoint = tripForm.EndPoint,
                    DepartureTime = departureTime,
                    ImagePath = tripForm.ImagePath,
                    Seats = tripForm.Seats,
                    Description = tripForm.Description
                };
                data.Trips.Add(newTrip);
                data.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
           
            return true;
        }

        public (bool isValid, string errors) ValidateTripAddForm(TripAddForm tripForm)
        {
            bool isValid = true;
            var errors = new StringBuilder();

            var checkDateTime = DateTime.TryParseExact(tripForm.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureTime);
            if (!checkDateTime)
            {
                isValid = false;
                errors.AppendLine("Departure time must be valid!");
            }
            if (tripForm.StartPoint == null)
            {
                isValid = false;
                errors.AppendLine("Starting point must be declarated!");
            }
            if(tripForm.EndPoint == null)
            {
                isValid = false;
                errors.AppendLine("End point must be declarated!");
            }
            
            if (tripForm.Seats < TRIP_SEATS_MIN_VALUE || tripForm.Seats > TRIP_SEATS_MAX_VALUE)
            {
                isValid = false;
                errors.AppendLine($"Number of seats must be betwwen {TRIP_SEATS_MIN_VALUE} and {TRIP_SEATS_MAX_VALUE}!");
            }
            if (tripForm.Description.Length > DESCRIPTION_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Description max length must be {DESCRIPTION_MAX_LENGTH}");
            }
            return (isValid, errors.ToString());
        }
        public ICollection<TripsListingModel> GetAllCreatedTrips()
        {
            var result = data.Trips.Select(t => new TripsListingModel
            {
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime.ToString(),
                Seats = t.Seats,
                Id = t.Id
            }).ToList();

            return result;
        }

        public TripDetailsModel GetSingleTripDetails(string tripId)
        {
            var result = data.Trips.Where(t => t.Id == tripId).Select(t => new TripDetailsModel
            {
                ImagePath = t.ImagePath,
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime.ToString("mm/dd/yyy hh:mm:ss"),
                Description = t.Description,
                Seats = t.Seats,
                Id = t.Id
            }).First();

            return result;
        }

        public (bool isValid, string error) AddUserToTrip(string tripId, string userId)
        {
            var checkMap = data.UserTrips.FirstOrDefault(ut => ut.UserId == userId && ut.TripId == tripId);
            if(checkMap != null)
            {
                return (false, "User is already joined!");
            }
            var trip = data.Trips.Where(t => t.Id == tripId).FirstOrDefault();
            if(trip.Seats == 0)
            {
                return (false, "No availible set!");
            }
            trip.Seats -= 1;
            UserTrip map = new UserTrip()
            {
                TripId = tripId,
                UserId = userId
            };
            
            data.UserTrips.Add(map);
            data.SaveChanges();
            return (true, "");

        }
    }
}
