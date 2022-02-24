using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
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
        [MinLength(EMAIL_MIN_LENGTH)]
        [MaxLength(EMAIL_MAX_LENGTH)]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public ICollection<UserPlayer> UserPlayers { get; set; } = new HashSet<UserPlayer>();
    }
}
