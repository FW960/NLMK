namespace NLMK.Models;

public abstract class GeneralProjectModel
{
    /// <summary>
    /// Направление проекта
    /// </summary>
    public readonly ProjectsMetaData.Types Types;

    /// <summary>
    /// Код проекта
    /// </summary>
    public string Id { get; set; }

    public readonly string Document;
}