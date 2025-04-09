using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentACar.Data.Models;

namespace RentACar.Controllers;

/// <summary>
/// The HomeController class handles requests for the home page and other static pages of the application.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{HomeController}"/> for logging purposes.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles requests to the home page.
    /// </summary>
    /// <returns>A <see cref="ViewResult"/> that renders the Index view.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Handles requests to the Privacy page.
    /// </summary>
    /// <returns>A <see cref="ViewResult"/> that renders the Privacy view.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Handles requests to the Error page.
    /// This method is used to display error information when an unhandled exception occurs.
    /// </summary>
    /// <returns>A <see cref="ViewResult"/> that renders the Error view with an <see cref="ErrorViewModel"/>.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
