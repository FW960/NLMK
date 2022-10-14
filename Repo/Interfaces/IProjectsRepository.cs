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
    public bool RemoveObject(string objectId);
    public bool GetProject(string projectId, out Project project);
    public bool AddObject(ProjectObject projectObject);
}