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
            var projects = _dbContext.Projects.ToList();

            _dbContext.ChangeTracker.Clear();

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
            _dbContext.ChangeTracker.Clear();
            var result = _dbContext.ProjectObjects.Update(projectObject);

            if (result.State == EntityState.Modified)
            {
                _dbContext.SaveChanges();
                _dbContext.ChangeTracker.Clear();

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool RemoveObject(int objectId)
    {
        try
        {
            _dbContext.ChangeTracker.Clear();
            var result = _dbContext.ProjectObjects.Remove(new ProjectObject {ObjectId = objectId});

            if (result.State == EntityState.Deleted)
            {
                _dbContext.SaveChanges();
                var childObjects = _dbContext.ProjectObjects.Where(x => x.RelatedObjectId == objectId).ToList();

                if (childObjects.Count > 0)
                {
                    foreach (var childObject in childObjects)
                    {
                        RemoveObject(childObject.ObjectId);
                    }
                }


                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool GetProject(int projectId, out Project project)
    {
        try
        {
            var result = _dbContext.Projects.Find(projectId);

            if (result is not null)
            {
                project = result;

                _dbContext.ChangeTracker.Clear();

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
            var result = _dbContext.ProjectObjects.Add(projectObject);

            if (result.State == EntityState.Added)
            {
                _dbContext.SaveChanges();

                _dbContext.ChangeTracker.Clear();
                
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool GetAllProjectInfo(int projectId, out Project project)
    {
        try
        {
            project = _dbContext.Projects.Find(projectId);

            if (project is not null)
            {
                project.ChildObjects = _dbContext.ProjectObjects
                    .Where(x => x.RelatedProjectId == projectId && x.RelatedObjectId == null).OrderBy(x => x.Order)
                    .ToList();

                project.ChildObjects.ForEach(x => x.ChildObjects = new List<ProjectObject>());

                FillProjectInfo(project.ChildObjects);

                _dbContext.ChangeTracker.Clear();

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            project = new Project();
            return false;
        }
    }

    public bool Export(List<ProjectObject> projectObjects)
    {
        try
        {
            
            foreach (var childObject in projectObjects)
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.ProjectObjects.Attach(childObject);
                _dbContext.Entry(childObject).Property(x => x.LinkedDocuments).IsModified = true;
                _dbContext.Entry(childObject).Property(x => x.LinkedDocumentsPerHierarchy).IsModified = true;
                _dbContext.SaveChanges();

                if (childObject.ChildObjects.Count != 0)
                    Export(childObject.ChildObjects);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    private void FillProjectInfo(List<ProjectObject> relatedObjects)
    {
        foreach (var relatedObject in relatedObjects)
        {
            try
            {
                relatedObject.ChildObjects = _dbContext.ProjectObjects
                    .Where(x => x.RelatedObjectId == relatedObject.ObjectId).OrderBy(x => x.Order).ToList();

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