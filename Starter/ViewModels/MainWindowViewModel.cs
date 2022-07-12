using System.Windows;
using Starter.Commands;
using Starter.ViewModels.Base;
using System.Windows.Input;
using System.Data;
using System.IO;
using Microsoft.Win32;
using Starter.Models.Data;

namespace Starter.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommand, CanCloseApplicationCommand);
            AddCsvFileCommand = new RelayCommand(OnAddCsvFileCommand, CanAddCsvFileCommand);
        }

        #region Commands

        #region Close
        public ICommand CloseApplicationCommand { get; }

        public bool CanCloseApplicationCommand(object p) => true;
        public void OnCloseApplicationCommand(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region AddCsvFileToDb

        public ICommand AddCsvFileCommand { get; }

        public bool CanAddCsvFileCommand(object p) => true;
        public void OnAddCsvFileCommand(object p)
        {
            string FileName = "DataTest";
            string ReadCSV = File.ReadAllText("D:\\Program_works\\git\\Starter\\Starter\\DataTest.csv");
            //string FileName;
            //string ReadCSV = File.ReadAllText(OpenFileDialog(out FileName));

            DataTable tblcsv = new DataTable();
            tblcsv.Columns.Add("Date");
            tblcsv.Columns.Add("Name");
            tblcsv.Columns.Add("SecondName");
            tblcsv.Columns.Add("Patranomic");
            tblcsv.Columns.Add("City");
            tblcsv.Columns.Add("Country");

            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    tblcsv.Rows.Add();
                    int count = 0;
                    foreach (string FileRec in csvRow.Split(';'))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                        count++;
                    }
                }
            }
            DataWorker.InsertCSVRecords(tblcsv, FileName);
        }
        public string OpenFileDialog(out string FileName)
        {
            var dialog = new OpenFileDialog { InitialDirectory = "C:\\" };
            dialog.ShowDialog();
            FileName = dialog.SafeFileName;
            return dialog.FileName;
        }
        #endregion

        #endregion
    }
}
