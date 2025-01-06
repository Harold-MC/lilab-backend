using Lilab.Data.ViewModel;

namespace Lilab.Service.Contract;

public interface IAuthService
{
    Task<LoginResponseViewModel> AuthenticateAsync(LoginViewModel model);
}