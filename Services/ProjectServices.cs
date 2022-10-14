using NLMK.Models;
using NLMK.Repo;
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

    public bool RemoveObject(string objectId)
    {
        if (_projectsRepository.RemoveObject(objectId))
            return true;

        return false;
    }

    public bool GetProject(string projectId, out Project project)
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
}