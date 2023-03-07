using System.Reflection;
using FlightScrapper.Core.Models;
using FlightsScrapper.Workbook.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace FlightsScrapper.Workbook;

public static class ExcelWriter
{
    static ExcelWriter()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public static void Write(IEnumerable<Flight> flights, IEnumerable<Trip> trips)
    {
        string filePath = $"{DateTime.Now:yyyyMMddTHHmmss}.xlsx";
        using var excelPackage = new ExcelPackage(new FileInfo(filePath));
        AddTripsWorksheet(excelPackage, trips);
        AddFlightsWorksheet(excelPackage, flights);

        excelPackage.Save();
    }

    private static void AddFlightsWorksheet(ExcelPackage excelPackage, IEnumerable<Flight> flights)
    {
        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Flights");
        IEnumerable<FlightExcelModel> flightsExcelModels = flights.Select(flight => new FlightExcelModel(flight));
        worksheet.Cells.LoadFromCollection(flightsExcelModels, true, TableStyles.Light1);
        worksheet.Columns.AutoFit();
    }

    private static void AddTripsWorksheet(ExcelPackage excelPackage, IEnumerable<Trip> trips)
    {
        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Trips");
        IEnumerable<TripExcelModel> tripsExcelModels = trips.Select(trip => new TripExcelModel(trip));
        worksheet.Cells.LoadFromCollection(tripsExcelModels, true, TableStyles.Light1);
        //worksheet.Columns.AutoFit();
    }
}