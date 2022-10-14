namespace NLMK.Models;

public class ProjectObject : GeneralProjectModel
{
    public readonly string RelatedProjectId;
    
    public readonly string? RelatedObjectId;
    
    public readonly int WorkingHoursStandard;
    
    public int WorkedInHours { get; set; }
    
    public readonly int TotalWorkingHours;

    public readonly ProjectsMetaData.Stages Stage;
    
    public ProjectsMetaData.Types Type;
    
    public List<ProjectObject> ChildObjects { get; set; }
    
    public readonly string Name;

    public ProjectObject(string relatedProjectId, int workingHoursStandard,
        int totalWorkingHours, string name, ProjectsMetaData.Types type, ProjectsMetaData.Stages stage)
    {
        RelatedProjectId = relatedProjectId;
        WorkingHoursStandard = workingHoursStandard;
        TotalWorkingHours = totalWorkingHours;
        Name = name;
        Type = type;
        Stage = stage;
    }

    public ProjectObject(string id)
    {
        Id = id;
    }
}