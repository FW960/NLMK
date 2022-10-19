using Microsoft.AspNetCore.Mvc;
using NLMK.Helpers;
using NLMK.Models;
using NLMK.Repo;
using NLMK.Repo.Interfaces;
using NLMK.Services.Interfaces;

namespace NLMK.Services;

public class ProjectServices : IProjectServices
{
    private readonly ProjectsRepository _projectsRepository;

    public ProjectServices(ProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }


    public bool AddObject(ProjectObject projectObject)
    {
        if (_projectsRepository.AddObject(projectObject))
            return true;

        return false;
    }

    public bool PatchObject(ProjectObject projectObject)
    {
        if (_projectsRepository.PatchObject(projectObject))
            return true;

        return false;
    }

    public bool RemoveObject(int objectId)
    {
        if (_projectsRepository.RemoveObject(objectId))
            return true;

        return false;
    }

    public bool GetProject(int projectId, out Project project)
    {
        if (_projectsRepository.GetProject(projectId, out project))
        {
            return true;
        }

        return false;
    }

    public List<Project> GetProjects()
    {
        return _projectsRepository.GetProjects();
    }

    public bool GetAllProjectInfo(int projectId, Controller controller, out ProjectResponse projectResponse)
    {
        projectResponse = new ProjectResponse();

        if (_projectsRepository.GetAllProjectInfo(projectId, out Project project))
        {
            var partialViews = RenderProjectPartialViews(controller, project);

            projectResponse = new ProjectResponse
            {
                Project = project,
                HtmlHierarchyPartial = partialViews.HtmlHierarchyPartial,
                HtmlTablePartial = partialViews.HtmlTablePartial
            };

            return true;
        }

        return false;
    }

    public bool Export(Project request)
    {
        if (_projectsRepository.Export(request.ChildObjects))
        {
            string htmlTable = MyHtmlHelper.GenerateTable(request);
            
            FileGenerator.SaveHtmlFile(request, htmlTable);
            
            FileGenerator.GeneratePdf(request.Document);
            
            FileGenerator.GenerateExcel(request.Document);
            
            return true;
        }
        else
        {
            return false;
        }
    }


    public (string HtmlHierarchyPartial, string HtmlTablePartial) RenderProjectPartialViews(Controller controller,
        Project project)
    {
        string HtmlHierarchyPartial =
            MyHtmlHelper.RenderViewToStringAsync(controller, "Partial/HierarchyPartial", project).Result;

        string HtmlTablePartial =
            MyHtmlHelper.RenderViewToStringAsync(controller, "Partial/TablePartial", project).Result;

        return (HtmlHierarchyPartial, HtmlTablePartial);
    }
}