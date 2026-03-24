using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ToDoPlatform.Helpers;
using ToDoPlatform.Models;
using ToDoPlatform.ViewModels;

namespace ToDoPlatform.Services;

public class UserService : IUserService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserService> _logger;

    public UserService(
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        ILogger<UserService> logger
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<SignInResult> Login(LoginVM login)
    {
        string userName = login.Email;
        if (Helper.IsValidEmail(login.Email))
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
                userName = user.UserName;
        }

        var result = await _signInManager.PasswordSignInAsync(
            userName, login.Password, login.RememberMe, lockoutOnFailure: true
        );

        if (result.Succeeded)
            _logger.LogInformation("");
        if (result.IsLockedOut)
            _logger.LogWarning("");

        return result;
    }

    public async Task Logout()
    {
        _logger.LogInformation($"Usuário '{ClaimTypes.Email}' saiu do sistema");
        await _signInManager.SignOutAsync();
    }
}
