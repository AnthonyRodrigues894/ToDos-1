using Microsoft.AspNetCore.Mvc;
using ToDoPlatform.Services;
using ToDoPlatform.ViewModels;

namespace ToDoPlatform.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;
    public AccountController(ILogger<AccountController> logger,
IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        LoginVM loginVM = new()
        {
            ReturnUrl = returnUrl ?? Url.Content("~/")
        };
        return View(loginVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.Login(loginVM);
            if (result.Succeeded)
                TempData["Success"] = "Login realizado com sucesso! Redirecionando...";

            else if (result.IsLockedOut)
                TempData["Failure"] = "Usuário bloqueado por muitas tentativas.";
            else if (result.IsNotAllowed)
                TempData["Failure"] = "Usuário sem permissão para acessar o sistema.";
            else
                TempData["Failure"] = "E-mail ou senha incorretos. Tente novamente.";
        }
        else
            TempData["Failure"] = "Dados inválidos. Verifique os campos preenchidos.";

            return View(loginVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();
        return RedirectToAction("Login", "Account");
    }
    public IActionResult Register() => View();
    public IActionResult Profile() => View();
}
