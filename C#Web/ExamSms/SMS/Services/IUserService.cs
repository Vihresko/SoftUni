using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    public interface IUserService
    {
        public (bool isValid, string errors) ValidateRegisterForm(User userFromPost);
        public void CreateNewUser(User userFromPost);
        public (string userId, string errors) ValidateLoginForm(User userFromPost);
    }
}
