using Microsoft.AspNetCore.Mvc;
using ToDoPlatform.Services;
using ToDoPlatform.ViewModels;

namespace ToDoPlatform.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;

    public AccountController(
        ILogger<AccountController> logger,
        IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        var loginVM = new LoginVM
        {
            ReturnUrl = returnUrl ?? Url.Content("~/")
        };

        return View(loginVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            TempData["Failure"] = "Dados inválidos. Verifique os campos preenchidos.";
            return View(loginVM);
        }

        var result = await _userService.Login(loginVM);

        if (result.Succeeded)
        {
            _logger.LogInformation("Login realizado com sucesso");

            TempData["Success"] = "Login realizado com sucesso! Redirecionando...";

            // 🔁 Redireciona corretamente após login
            if (!string.IsNullOrEmpty(loginVM.ReturnUrl) && Url.IsLocalUrl(loginVM.ReturnUrl))
                return Redirect(loginVM.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        if (result.IsLockedOut)
        {
            TempData["Failure"] = "Usuário bloqueado por muitas tentativas.";
        }
        else if (result.IsNotAllowed)
        {
            TempData["Failure"] = "Usuário sem permissão para acessar o sistema.";
        }
        else
        {
            TempData["Failure"] = "E-mail ou senha incorretos. Tente novamente.";
        }

        return View(loginVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();

        _logger.LogInformation("Usuário saiu do sistema");

        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View();
    }
}