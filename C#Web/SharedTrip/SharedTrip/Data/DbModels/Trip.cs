namespace SharedTrip.Data.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Constrains.Constant;
    public class Trip
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; init; }

        [Required]
        public string EndPoint { get; init; }
        public DateTime DepartureTime { get; init; }

        [Range(TRIP_SEATS_MIN_VALUE, TRIP_SEATS_MAX_VALUE)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(DESCRIPTION_MAX_LENGTH)]
        public string Description{ get; init; }
        public string ImagePath { get; init; }
        public ICollection<UserTrip> UserTrip { get; set; } = new HashSet<UserTrip>();

    }
}
