using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NLMK.Models;

namespace NLMK.Helpers;

public static class HtmlHelper
{
    public static string WriteProjectHierarchyDivElement(ProjectObject projectObject)
    {
        string divElement =
            $@"<div class='object' stage='{(int) projectObject.Stage}' type='{(int) projectObject.Type}' id='{projectObject.ObjectId}' style='display: block; margin-left: 5px;' onclick='displayObject({projectObject.ObjectId})'>
        {projectObject.Name}";

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
        {projectObject.Name}";

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

    public static string WriteProjectTable(ProjectObject project)
    {
        string tableData = $@"<tr>
            <td>{ProjectsMetaData.Localize(project.Type)}</td>
            <td>{project.ObjectId}</td>
            <td>{project.Document}</td>
            <td>{project.WorkingHoursStandard}</td>
            <td>
                <input type='number' value='{project.WorkedInHours}'>
            </td>
            <td>{project.TotalWorkingHours}</td>
            </tr>";

        if (project.ChildObjects.Count == 0)
        {
            return tableData;
        }
        else
        {
            foreach (var childObject in project.ChildObjects)
            {
                tableData += WriteProjectTable(childObject);
            }

            return tableData;
        }
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

                ViewEngineResult viewResult = null;

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