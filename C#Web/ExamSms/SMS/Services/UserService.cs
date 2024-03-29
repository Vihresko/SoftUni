﻿using SMS.Data;
using SMS.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SMS.Services
{
    using static SMS.Data.Constant;
    public class UserService : IUserService
    {
        private readonly SMSDbContext data;
        public UserService(SMSDbContext _data)
        {
            this.data = _data;
        }
        public void CreateNewUser(User userPostForm)
        {
            var cart = new Data.DbModels.Cart();

            Data.DbModels.User newUser = new Data.DbModels.User()
            {
                Username = userPostForm.Username,
                Email = userPostForm.Email,
                Password = HashPassword(userPostForm.Password),
                Cart = cart,
                CartId = cart.Id
            };
            
            data.Users.Add(newUser);
            data.SaveChanges();
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
        public (bool isValid, string errors) ValidateRegisterForm(User user)
        {
            bool isValid = true;
            StringBuilder errors = new StringBuilder();
            if(string.IsNullOrWhiteSpace(user.Username) || user.Username.Length < USER_NAME_MIN_LENGTH || user.Username.Length > USER_NAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Username length must be between {USER_NAME_MIN_LENGTH} and {USER_NAME_MAX_LENGTH} symbols!");
            }

            if(!Regex.IsMatch(user.Email, EMAIL_REGEX_VALIDATION))
            {
                isValid &= false;
                errors.AppendLine($"Email must be valid!");
            }

            if(string.IsNullOrWhiteSpace(user.Password) ||user.Password.Length < PASS_MIN_LENGTH || user.Password.Length > PASS_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Password must be between {PASS_MIN_LENGTH} and {PASS_MAX_LENGTH} symbols!");
            }
            if(user.Password != user.ConfirmPassword)
            {
                isValid = false;
                errors.AppendLine($"Password and ConfirmPassword must be equal!");
            }

            return (isValid, errors.ToString());
        }

        public (string userId, string errors) ValidateLoginForm(User userFromPost)
        {
            string passWord = HashPassword(userFromPost.Password);
            string userName = userFromPost.Username;

            string user = data.Users.Where(u => u.Username == userName && u.Password == passWord)
                                    .Select(u => u.Id)
                                    .FirstOrDefault();

            if(user == null)
            {
                return (user, "Invalid authentication!");
            }

            return (user, "");
        }
    }
}
