namespace NLMK.Models;

public class Project : GeneralProjectModel
{
    public string Name { get; set; }
    public List<ProjectObject> ChildObjects { get; set; }
}