using System.ComponentModel.DataAnnotations;

namespace NLMK.Models;

public class ProjectObject
{
    [Key] public int ObjectId { get; set; }

    public string Name { get; set; }

    public int? RelatedObjectId { get; set; }

    public int RelatedProjectId { get; set; }

    public string Document { get; set; }

    public ProjectsMetaData.Types Type { get; set; }

    public ProjectsMetaData.Stages Stage { get; set; }

    public int WorkingHoursStandard { get; set; }

    public int Order { get; set; }
    public int LinkedDocuments { get; set; }

    private int _linkedDocumentsPerHierarchy;
    public int LinkedDocumentsPerHierarchy
    {
        get
        {
            int linkedDocumentsPerHierarchy = 0;
            foreach (var child in ChildObjects)
            {
                linkedDocumentsPerHierarchy += child.LinkedDocumentsPerHierarchy;
            }

            _linkedDocumentsPerHierarchy =  LinkedDocuments + linkedDocumentsPerHierarchy;

            return _linkedDocumentsPerHierarchy;
        }
        set { _linkedDocumentsPerHierarchy = value; }
    }

    public List<ProjectObject> ChildObjects { get; set; }
}