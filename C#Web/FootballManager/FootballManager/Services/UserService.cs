using FootballManager.Constrains;
using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace FootballManager.Services
{
    using static Constrains.Constant;
    public class UserService : IUserService
    {
        private readonly FootballManagerDbContext data;
        public UserService(FootballManagerDbContext _data)
        {
            this.data = _data;
        }
        public (bool isValid, string errors) ValidateRegisterForm(RegisterFormModel model)
        {
            bool isValid = true;
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(model.Username) ||model.Username.Length < USERNAME_MIN_LENGTH || model.Username.Length > USERNAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Username must be between {USERNAME_MIN_LENGTH} and {USERNAME_MAX_LENGTH} symbols!");
            }
            if (model.Email == null)
            {
                isValid = false;
                errors.AppendLine("Email must be valid!");
            }
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < PASSWORD_MIN_LENGTH || model.Password.Length > PASSWORD_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Password must be between {PASSWORD_MIN_LENGTH} and {PASSWORD_MAX_LENGTH} symbols!");
            }
            if (model.ConfirmPassword != model.Password)
            {
                isValid = false;
                errors.AppendLine("'Confirm password' and 'Password' must be same!");
            }
            return (isValid, errors.ToString());
        }

        public bool CreateNewUser(RegisterFormModel model)
        {
            bool isDone = false;
            var newUser = new User()
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashPassword(model.Password)
            };

            try
            {
                data.Users.Add(newUser);
                data.SaveChanges();
                isDone = true;
            }
            catch { }
            return isDone;
        }

        private string HashPassword(string rawPassword)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public (bool isRegistered, string userId, string errors) ValidateLoginForm(LoginFormModel model)
        {
            string hashedPassowordFromForm = HashPassword(model.Password);
            var userId = data.Users.Where(u => u.Username == model.Username && u.Password == hashedPassowordFromForm)
                                   .Select(u => u.Id).FirstOrDefault();
            if(userId == null)
            {
                return (false, null, "Wrong authentication!");
            }
            return (true, userId, "");
        }
    }
}
