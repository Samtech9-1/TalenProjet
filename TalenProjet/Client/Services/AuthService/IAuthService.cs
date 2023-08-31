using TalenProjet.Shared;

namespace TalenProjet.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);

        Task<ServiceResponse<string>> LogIn(UserLogIn request);

        Task<ServiceResponse<bool>> ChangePassword(UserChangePasseword request);
    }
}
