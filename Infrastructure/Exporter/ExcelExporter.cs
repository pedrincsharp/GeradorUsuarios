using ClosedXML.Excel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exporter;

public static class ExcelExporter
{
    public static Stream Export<T>(List<T> models)
    {
        var workbook = new XLWorkbook();
        var type = typeof(T);
        var properties = type.GetProperties();
        var worksheet = workbook.Worksheets.Add(type.Name);

        //Cabeçalho
        for (int i = 0; i < properties.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = properties[i].Name;
        }

        var headerRange = worksheet.Range(1, 1, 1, properties.Length);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

        //Dados
        for (int row = 0; row < models.Count; row++)
        {
            for (int col = 0; col < properties.Length; col++)
            {
                var value = properties[col].GetValue(models[row]);
                worksheet.Cell(row + 2, col + 1).Value = value?.ToString() ?? "";
            }
        }

        worksheet.Columns().AdjustToContents();

        var stream = new MemoryStream();
        workbook.SaveAs(stream);

        stream.Position = 0;
        return stream;
    }

}