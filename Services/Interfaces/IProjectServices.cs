using NLMK.Models;

namespace NLMK.Services.Interfaces;

public interface IProjectServices
{
    public bool AddObject(ProjectObject projectObject);

    public bool PatchObject(ProjectObject projectObject);
    
    public bool RemoveObject(string objectId);

    public bool GetProject(string projectId, out Project project);

    public List<Project> GetProjects();
}