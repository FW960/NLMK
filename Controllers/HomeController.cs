using Microsoft.AspNetCore.Mvc;
using NLMK.Helpers;
using NLMK.Models;
using NLMK.Services;
using NLMK.Services.Interfaces;

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

    [HttpGet]
    [Route("{action}/id={id}")]
    public IActionResult GetAllProjectInfo([FromRoute] int id)
    {
        if (_services.GetAllProjectInfo(id, this, out ProjectResponse project))
        {
            return Ok(project);
        }

        return BadRequest();
    }

    [HttpPatch]
    [Route("{action}")]
    public ActionResult PatchObject([FromBody] ProjectObject projectObject)
    {
        if (_services.PatchObject(projectObject))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("{action}")]
    public ActionResult AddObject([FromBody] ProjectObject projectObject)
    {
        if (_services.AddObject(projectObject))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete]
    [Route("{action}/id={id}")]
    public ActionResult RemoveObject([FromRoute] int id)
    {
        if (_services.RemoveObject(id))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("{action}")]
    public ActionResult Export([FromBody] Project request)
    {
        if (_services.Export(request))
        {
            return Ok();
        }

        return BadRequest();
    }
}