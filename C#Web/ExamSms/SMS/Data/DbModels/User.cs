using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.DbModels
{
    public class User
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(Constant.USER_NAME_MIN_LENGTH)]
        [MaxLength(Constant.USER_NAME_MAX_LENGTH)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(Constant.EMAIL_REGEX_VALIDATION)]
        public string Email { get; set; }

        [Required]
        [MaxLength(Constant.PASS_HASH_LENGTH)]
        public string Password { get; set; }

        [Required]
        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
