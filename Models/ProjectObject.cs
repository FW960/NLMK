namespace NLMK.Models;

public class Object : GeneralModel
{
    public readonly string RelatedProjectId;
    
    public readonly string? RelatedObjectId;
    
    public readonly int WorkingHoursStandard;
    public int WorkedInHours { get; set; }
    
    public readonly int TotalWorkingHours;

    public readonly ProjectsMetaData.Stages Stage;
    public List<Object> ChildObjects { get; set; }
    
    public readonly string Name;

    public Object(string relatedProjectId, int workingHoursStandard,
        int totalWorkingHours, string name, ProjectsMetaData.Types type, ProjectsMetaData.Stages stage) : base(type)
    {
        RelatedProjectId = relatedProjectId;
        WorkingHoursStandard = workingHoursStandard;
        TotalWorkingHours = totalWorkingHours;
        Name = name;
        Stage = stage;
    }

    public Object(string id)
    {
        Id = id;
    }
}