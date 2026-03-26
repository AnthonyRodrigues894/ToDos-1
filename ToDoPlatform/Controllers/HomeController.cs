using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoPlatform.Models;

namespace ToDoPlatform.Controllers;

[Authorize]
public class HomeController : Controller
{
    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };

        return View(model);
    }
}