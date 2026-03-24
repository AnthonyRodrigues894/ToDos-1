using Microsoft.AspNetCore.Identity;
using ToDoPlatform.ViewModels;

namespace ToDoPlatform.Services;

public interface IUserService
{
    Task<SignInResult> Login(LoginVM login);
    Task Logout();
}
