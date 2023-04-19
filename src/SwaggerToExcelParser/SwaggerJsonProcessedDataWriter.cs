using System.Data;
using ClosedXML.Excel;

namespace SwaggerToExcelParser;

public sealed class SwaggerJsonProcessedDataWriter
{
    public async Task Save(IAsyncEnumerable<Endpoint> endpoints, string fileName)
    {
        using var dataTable = new DataTable();
        dataTable.Columns.Add("Method", typeof(string));
        dataTable.Columns.Add("Path", typeof(string));

        await foreach (var endpoint in endpoints)
        {
            dataTable.Rows.Add(endpoint.Method, endpoint.Path);
        }
        
        using var workbook = new XLWorkbook();
        var ws = workbook.AddWorksheet();
        ws.Cell("A2").InsertData(dataTable);
        workbook.SaveAs(fileName);
    }
}
