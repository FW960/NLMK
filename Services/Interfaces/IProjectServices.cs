using Microsoft.AspNetCore.Mvc;
using NLMK.Helpers;
using NLMK.Models;

namespace NLMK.Services.Interfaces;

public interface IProjectServices
{
    public bool AddObject(ProjectObject projectObject);

    public bool PatchObject(ProjectObject projectObject);

    public bool RemoveObject(int objectId);

    public bool GetProject(int projectId, out Project project);

    public List<Project> GetProjects();

    public bool GetAllProjectInfo(int projectId, Controller controller, out ProjectResponse projectResponse);
    public bool Export(Project project);
    public (string HtmlHierarchyPartial, string HtmlTablePartial) RenderProjectPartialViews(Controller controller,
        Project project);
}