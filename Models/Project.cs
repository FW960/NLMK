using System.ComponentModel.DataAnnotations;

namespace NLMK.Models;

public class Project
{
    [Key]
    public int ProjectId { get; set; }
    public string Document { get; set; }
    public string Name { get; set; }
    public List<ProjectObject> ChildObjects { get; set; }
}

