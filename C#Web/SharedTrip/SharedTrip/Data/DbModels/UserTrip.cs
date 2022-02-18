namespace SharedTrip.Data.DbModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class UserTrip
    {
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; init; }
        public User User { get; set; }

        [ForeignKey(nameof(Trip))]
        [Required]
        public string TripId { get; init; }
        public Trip Trip { get; set; }
    }
}
