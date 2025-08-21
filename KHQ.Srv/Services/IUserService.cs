using KHQ.Domain.DTOs;

namespace KHQ.Srv.Services
{
    public interface IUserService
    {
        Task Register(UserRegister viewModel);

        Task<ValidateUserResult> Login(UserLogin viewModel);

        Task Logout();
    }
}
