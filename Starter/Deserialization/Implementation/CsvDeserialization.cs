using Starter.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Starter.Deserialization.Implementation
{
    public class CsvDeserialization<T> : IDeserialization<T> where T : new()
    {
        public async Task<List<T>> Deserialization(string filePath)
        {
            //string ReadCSV = File.ReadAllText(OpenFileDialog());
            string ReadCSV = await File.ReadAllTextAsync(filePath);

            List<T> result = new List<T>();
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    object exemplar = new T();

                    string[] allData =  csvRow.Split(';');
                    PropertyInfo[] allFields = typeof(T).GetFilteredProperties();

                    for (int i = 0; i < allData.Length; i++)
                    {
                        string FileRec = allData[i];
                        PropertyInfo? field = allFields[i];
                        field.SetValue(exemplar, FileRec);
                    }

                    result.Add((T)exemplar);
                }
            }
            return result;
        }

        public void Dispose()
        {

        }
    }
}
