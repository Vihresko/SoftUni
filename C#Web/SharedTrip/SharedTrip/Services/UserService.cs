using SharedTrip.Data;
using SharedTrip.Data.DbModels;
using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services
{
    using static Constrains.Constant;
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        public UserService(ApplicationDbContext dbContext)
        {
            this.data = dbContext;
        }
        public (string userId, string error) LoginValidate(LoginPostModel model)
        {
            string passWord = HashThePassword(model.Password);
            string userName = model.Username;

            string user = data.Users.Where(u => u.Username == userName && u.Password == passWord).Select(u => u.Id).FirstOrDefault();
            if (user == null)
            {
                return (user, "Invalid authentication");
            }
            return (user, "");
        }
        public (bool isValid, string errors) CreateUser(RegisterPostModel model)
        {
            bool isValid = true;
            var errors = new StringBuilder();
            var newUser = new User()
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashThePassword(model.Password),
            };
            var isUserAvailible = data.Users.Where(u => u.Username == newUser.Username).FirstOrDefault();
            if(isUserAvailible != null)
            {
                isValid = false;
                errors.AppendLine("This Username is not availible!");
                return (isValid, errors.ToString());
            }
            data.Users.Add(newUser);
            data.SaveChanges();
            return (isValid, "");
        }

        public (bool isValid, string errors) ValidateRegisterPostmodel(RegisterPostModel model)
        {
            bool isValid = true;
            var errors = new StringBuilder();

           if(model.Username.Length < USERNAME_MIN_LENGTH || model.Username.Length > USERNAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Username must be between {USERNAME_MIN_LENGTH} and {USERNAME_MAX_LENGTH} symbols!");
            }
           if(model.Email == null)
            {
                isValid = false;
                errors.AppendLine("Email must be valid");
            }
           if(model.Password.Length < PASSWORD_MIN_LENGTH || model.Password.Length > PASSWORD_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Password must be between {PASSWORD_MIN_LENGTH} and {PASSWORD_MAX_LENGTH} symbols!");
            }
           if(model.ConfirmPassword != model.Password)
            {
                isValid = false;
                errors.AppendLine("'Confirm password' and 'Password' must be same!");
            }
            return (isValid, errors.ToString());
        }

        static string HashThePassword(string passwordString)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passwordString));

                // Convert byte array to a string   
                StringBuilder passwordHash = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    passwordHash.Append(bytes[i].ToString("x2"));
                }
                return passwordHash.ToString();
            }
        }

    }
}
