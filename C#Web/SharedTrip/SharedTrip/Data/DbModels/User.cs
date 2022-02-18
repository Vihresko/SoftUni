namespace SharedTrip.Data.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Constrains.Constant;
    public class User
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(USERNAME_MIN_LENGTH)]
        [MaxLength(USERNAME_MAX_LENGTH)]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        [MaxLength(PASSWORD_HASH_LENGTH)]
        public string  Password { get; init; }
        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

    }
}
