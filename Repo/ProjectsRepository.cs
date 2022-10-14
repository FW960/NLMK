using Microsoft.EntityFrameworkCore;
using NLMK.DBContext;
using NLMK.Models;
using NLMK.Repo.Interfaces;

namespace NLMK.Repo;

public class ProjectsRepository : IProjectsRepository
{
    private readonly ProjectsDbContext _dbContext;

    public ProjectsRepository(ProjectsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Project> GetProjects()
    {
        try
        {
            var projects = _dbContext.Set<Project>().ToList();

            return projects;
        }
        catch
        {
            return new List<Project>();
        }
    }

    public bool PatchObject(ProjectObject projectObject)
    {
        try
        {
            var result = _dbContext.Set<ProjectObject>().Update(projectObject);

            if (result.State == EntityState.Modified)
                return true;

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool RemoveObject(string objectId)
    {
        try
        {
            var result = _dbContext.Set<ProjectObject>().Remove(new ProjectObject(objectId));

            if (result.State == EntityState.Deleted)
                return true;

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool GetProject(string projectId, out Project project)
    {
        try
        {
            var result = _dbContext.Set<Project>().Find(projectId);

            if (result is not null)
            {
                project = result;
                return true;
            }
            else
            {
                project = new Project();
                return false;
            }
        }
        catch
        {
            project = new Project();
            return false;
        }
    }

    public bool AddObject(ProjectObject projectObject)
    {
        try
        {
            var result = _dbContext.Set<ProjectObject>().Add(projectObject);

            if (result.State == EntityState.Added)
                return true;

            return false;
        }
        catch
        {
            return false;
        }
    }

    public Project GetAllProjectInfo(string projectId)
    {
        try
        {
            var project = _dbContext.Set<Project>().Find(projectId);

            if (project is not null)
            {
                project.ChildObjects = _dbContext.Set<ProjectObject>().Where(x => x.RelatedProjectId == project.Id)
                    .Select(x => new ProjectObject(projectId, x.WorkingHoursStandard, x.TotalWorkingHours, x.Name,
                        x.Type, x.Stage))
                    .ToList();

                project.ChildObjects.ForEach(x => x.ChildObjects = new List<ProjectObject>());

                FillProjectInfo(project.ChildObjects);

                return project;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private void FillProjectInfo(List<ProjectObject> relatedObjects)
    {
        foreach (var relatedObject in relatedObjects)
        {

            try
            {
                relatedObject.ChildObjects = _dbContext.Set<ProjectObject>()
                    .Where(x => x.RelatedObjectId == relatedObject.Id)
                    .Select(x => new ProjectObject(relatedObject.RelatedProjectId, x.WorkingHoursStandard,
                        x.TotalWorkingHours, x.Name, x.Type, x.Stage)).ToList();

                if (relatedObject.ChildObjects.Count != 0)
                {
                    relatedObject.ChildObjects.ForEach(x => x.ChildObjects = new List<ProjectObject>());
                    FillProjectInfo(relatedObject.ChildObjects);
                }
            }
            catch
            {
                //todo logger
                continue;
            }
            
        }
    }
}