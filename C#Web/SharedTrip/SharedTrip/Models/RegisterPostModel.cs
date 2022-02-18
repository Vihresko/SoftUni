namespace SharedTrip.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Constrains.Constant;
    public class RegisterPostModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword{ get; set; }
    }
}
