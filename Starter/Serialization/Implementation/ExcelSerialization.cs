using Microsoft.Office.Interop.Excel;
using Starter.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Starter.Serialization.Implementation
{
    public class ExcelSerialization<T> : ISerialization<T>
    {
        private Application _excel;
        private Workbook _workbook;

        public ExcelSerialization()
        {
            _excel = new Application();
            _excel.Visible = false;

            _workbook = _excel.Workbooks.Add();
        }

        public async Task Serialization(string filePath, object data)
        {
            await SetData((List<T>)data);
            if (!string.IsNullOrEmpty(filePath))
            {
                _workbook.SaveAs(filePath);
            }
        }

        private async Task<bool> Set(string column, int row, object data)
        {
            try
            {
                ((Worksheet)_excel.ActiveSheet).Cells[row, column] = data;
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        private async Task SetData(List<T> data)
        {
            try
            {
                char colomnValue = 'A';
                foreach (var field in typeof(T).GetFilteredProperties())
                {
                    await Set(column: colomnValue.ToString(), row: 1, data: field.Name);
                    colomnValue++;
                }


                int i = 2;
                foreach (var item in (IEnumerable<T>)data)
                {
                    colomnValue = 'A';
                    foreach (var field in typeof(T).GetFilteredProperties())
                    {
                        await Set(column: colomnValue.ToString(), row: i, data: field.GetValue(item));
                        colomnValue++;
                    }
                    i++;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void Dispose()
        {
            try
            {
                _workbook.Close();
                _excel.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
