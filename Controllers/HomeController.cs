using Microsoft.AspNetCore.Mvc;
using NLMK.Services;

namespace NLMK.Controllers;

public class HomeController : Controller
{
    private readonly ProjectServices _services;

    public HomeController(ProjectServices services)
    {
        _services = services;
    }

    public ActionResult Index()
    {
        var projects = _services.GetProjects();

        return View("Index", projects);
    }
}