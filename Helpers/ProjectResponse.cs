using NLMK.Models;

namespace NLMK.Helpers;

public class ProjectResponse
{
    public string HtmlHierarchyPartial { get; set; }
    
    public string HtmlTablePartial { get; set; }
    
    public Project Project { get; set; }
}