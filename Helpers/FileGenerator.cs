using System.Text;
using NLMK.Models;

namespace NLMK.Helpers;

using Aspose.Cells;

public static class FileGenerator
{
    public static void GeneratePdf(string path)
    {
        var workbook = new Workbook(path + ".html");
        workbook.FileFormat = FileFormatType.Excel4;
        workbook.Save("Output.pdf");
    }

    public static void GenerateExcel(string path)
    {
        var workbook = new Workbook(path + ".html");
        workbook.FileFormat = FileFormatType.Excel4;
        workbook.Save("Output.xls");
    }

    public static void SaveHtmlFile(Project project, string html)
    {
        using (var file = File.Create($"{project.Document}.html"))
        {
            file.Write(Encoding.UTF8.GetBytes(html));
        }
    }
}