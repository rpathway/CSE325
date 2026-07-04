using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();            
var storesDir = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);            

var salesFiles = FindFiles(storesDir);
var salesTotal = CalculateSalesTotal(salesFiles);
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

GenerateSalesReport(salesFiles, salesTotal);


string GenerateSalesReport(IEnumerable<string> salesFiles, double salesTotal)
{
    StringBuilder report = new StringBuilder();
 
    report.AppendLine("Sales Summary");
    report.AppendLine(new string('-', 28));
    report.AppendLine($" Total Sales: {salesTotal:C}");
    report.AppendLine();
    report.AppendLine(" Details:");

    foreach (var file in salesFiles)
    {
        string json = File.ReadAllText(file);
        var data = JsonConvert.DeserializeObject<SalesData>(json);
        var amount = data?.Total != 0 ? data.Total : JsonConvert.DeserializeObject<OverAllSalesData>(json)?.OverallTotal ?? 0;
        report.AppendLine($"  {Path.GetFileName(file)}: {amount:C}");
    }

    string outputPath = Path.Combine(salesTotalDir, "salesSummary.txt");
    File.WriteAllText(outputPath, report.ToString());

    return outputPath;
}


IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}


double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

record OverAllSalesData (double OverallTotal);
record SalesData (double Total);