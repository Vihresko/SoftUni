namespace SharedTrip.Services
{
    using SharedTrip.Models;
    public interface IUserService
    {
        public (string userId, string error) LoginValidate(LoginPostModel model);
        public (bool isValid, string errors) ValidateRegisterPostmodel(RegisterPostModel model);
        public (bool isValid, string errors) CreateUser(RegisterPostModel model);

    }
}
