using System.Reflection;
using FlightScrapper.Core;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace FlightsScrapper.Workbook;

public static class ExcelWriter
{
    public static void Write(IEnumerable<Flight> flights)
    {
        string filePath = $"Flights_{DateTime.Now.Ticks}.xlsx";

        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets.Add("Flights");
        worksheet.Cells.LoadFromCollection(flights, true, TableStyles.Light1);
        worksheet.Columns.AutoFit();
        package.Save();
    }
}