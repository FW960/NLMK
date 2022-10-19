using NLMK.Models;

namespace NLMK.Repo.Interfaces;

public interface IProjectsRepository
{
    /// <summary>
    /// Из базы достаются проекты без вложенных объектов.
    /// </summary>
    /// <returns></returns>
    public List<Project> GetProjects();
    public bool PatchObject(ProjectObject projectObject);
    public bool RemoveObject(int objectId);
    public bool GetProject(int projectId, out Project project);
    public bool AddObject(ProjectObject projectObject);
    public bool GetAllProjectInfo(int projectId, out Project project);
    public bool Export(List<ProjectObject> projectObjects);
}