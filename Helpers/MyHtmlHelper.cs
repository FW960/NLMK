using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NLMK.Models;

namespace NLMK.Helpers;

public static class MyHtmlHelper
{
    public static string WriteProjectHierarchyDivElement(ProjectObject projectObject)
    {
        string divElement =
            $@"<div class='object' stage='{(int) projectObject.Stage}' type='{(int) projectObject.Type}' id='{projectObject.ObjectId}' style='display: block; margin-left: 5px;' onclick='displayObject({projectObject.ObjectId})'>
        {projectObject.Name} {projectObject.Order}";

        if (projectObject.ChildObjects.Count == 0)
        {
            return divElement + "</div>";
        }
        else
        {
            foreach (var childObject in projectObject.ChildObjects)
            {
                divElement += WriteObjectHierarchyDivElement(childObject, 10);
            }

            return divElement + "</div>";
        }
    }

    private static string WriteObjectHierarchyDivElement(ProjectObject projectObject, int margin)
    {
        string divElement =
            $@"<div class='object' stage='{(int) projectObject.Stage}' type='{(int) projectObject.Type}' id='{projectObject.ObjectId}' style='display: none; margin-left: {margin}px;' onclick='displayObject({projectObject.ObjectId})'>
        {projectObject.Name} {projectObject.Order}";

        if (projectObject.ChildObjects.Count == 0)
        {
            return divElement + "</div>";
        }
        else
        {
            margin += 5;
            foreach (var childObject in projectObject.ChildObjects)
            {
                divElement += WriteObjectHierarchyDivElement(childObject, margin);
            }

            return divElement + "</div>";
        }
    }

    public static string WriteProjectTable(ProjectObject projectObject)
    {
        string tableData = $@"<tr type='{(int) projectObject.Type}' stage='{(int) projectObject.Stage}''>
        <td>{ProjectsMetaData.Localize(projectObject.Type)}</td>
        <td>{projectObject.ObjectId}</td>
        <td>{projectObject.Document}</td>
        <td>{projectObject.WorkingHoursStandard}</td>
        <td><input class='td-input' onchange='CalculateTable({projectObject.ObjectId})' id='td-{projectObject.ObjectId}' type='number' value ='{projectObject.LinkedDocuments}'/></td>
        <td>{projectObject.LinkedDocumentsPerHierarchy}</td>
        </tr>";

        if (projectObject.ChildObjects.Count == 0)
        {
            return tableData;
        }
        else
        {
            foreach (var childObject in projectObject.ChildObjects)
            {
                tableData += WriteProjectTable(childObject);
            }

            return tableData;
        }
    }

    public static string GenerateTable(Project project)
    {
        string table = @"<table>
        <thead>
            <tr>
            <td>Направление</td>
            <td>Код</td>
            <td>Документ</td>
            <td>Норматив чел./час.</td>
            <td>Кол-во</td>
            <td>Итого (все)</td>
            </tr>
        </thead>";
        table += "<tbody>";

        foreach (var childObject in project.ChildObjects)
        {
            table += GenerateTable(childObject);
        }

        table += "</tbody>";
        table += "</table>";

        return table;
    }

    private static string GenerateTable(ProjectObject projectObject)
    {
        string tableRow = $@"<tr>
        <td>{ProjectsMetaData.Localize(projectObject.Type)}</td>
        <td>{projectObject.ObjectId}</td>
        <td>{projectObject.Document}</td>
        <td>{projectObject.WorkingHoursStandard}</td>
        <td>{projectObject.LinkedDocuments}</td>
        <td>{projectObject.LinkedDocumentsPerHierarchy}</td>
        </tr>";

        if (projectObject.ChildObjects.Count > 0)
        {
            foreach (var childObject in projectObject.ChildObjects)
            {
                tableRow += GenerateTable(childObject);
            }
        }

        return tableRow;
    }


    public static async Task<string> RenderViewToStringAsync<TModel>(this Controller controller, string viewNamePath,
        TModel model)
    {
        if (string.IsNullOrEmpty(viewNamePath))
            viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

        controller.ViewData.Model = model;

        using (StringWriter writer = new StringWriter())
        {
            try
            {
                IViewEngine viewEngine =
                    controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as
                        ICompositeViewEngine;

                ViewEngineResult viewResult;

                if (viewNamePath.EndsWith(".cshtml"))
                    viewResult = viewEngine.GetView(viewNamePath, viewNamePath, false);
                else
                    viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

                if (!viewResult.Success)
                    return $"A view with the name '{viewNamePath}' could not be found";

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
            catch (Exception exc)
            {
                return $"Failed - {exc.Message}";
            }
        }
    }
}