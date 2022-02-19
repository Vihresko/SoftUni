using SMS.Models;

namespace SMS.Services
{
    public interface IUserService
    {
        public (bool isValid, string errors) ValidateRegisterForm(User userFromPost);
        public void CreateNewUser(User userFromPost);
        public (string userId, string errors) ValidateLoginForm(User userFromPost);
    }
}
