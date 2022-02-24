using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
    using static Constrains.Constant;
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(FULLNAME_MIN_LENGTH)]
        [MaxLength(FULLNAME_MAX_LENGTH)]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(POSITION_MIN_LENGTH)]
        [MaxLength(POSITION_MAX_LENGTH)]
        public string Position { get; set; }

        [Range(SPEED_MIN_VALUE, SPEED_MAX_VALUE)]
        public byte Speed { get; set; }

        [Range(ENDURANCE_MIN_VALUE, ENDURANCE_MAX_VALUE)]
        public byte Endurance { get; set; }

        [Required]
        [MaxLength(DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        public ICollection<UserPlayer> UserPlayers { get; set; } = new HashSet<UserPlayer>();
    }
}
