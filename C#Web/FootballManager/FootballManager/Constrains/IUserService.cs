using FootballManager.ViewModels;

namespace FootballManager.Constrains
{
    public interface IUserService
    {
        public (bool isValid, string errors) ValidateRegisterForm(RegisterFormModel model);
        public bool CreateNewUser(RegisterFormModel model);

        public (bool isRegistered,string userId, string errors) ValidateLoginForm(LoginFormModel model);
    }
}
