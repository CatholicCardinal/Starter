﻿using Microsoft.Win32;
using Starter.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Deserialization.Implementation
{
    public class CsvDeserialization<T> : IDeserialization<T> where T : class, new()
    {
        public List<T> Deserialization(string filePath)
        {
            //string ReadCSV = File.ReadAllText(OpenFileDialog());
            string ReadCSV = File.ReadAllText(filePath);

            List<T> result = new List<T>();
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    object exemplar = new T();
                    string[] allData = csvRow.Split(';');
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

        public string OpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = "C:\\" };
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            dialog.ShowDialog();

            return dialog.FileName;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}