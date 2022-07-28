using Microsoft.Win32;
using Starter.Commands;
using Starter.Deserialization.Implementation;
using Starter.Models;
using Starter.Models.Repositories;
using Starter.Serialization;
using Starter.ViewModels.Base;
using Starter.Views;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Starter.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private List<Record> allRecords;
        public List<Record> AllRecords { get => allRecords; set => Set(ref allRecords, value); }

        private List<Record> sampleExport = new List<Record>();
        public List<Record> SampleExport { get => sampleExport; set => Set(ref sampleExport, value); }

        private string typeSerialization;
        public string TypeSerialization { get => typeSerialization; set => Set(ref typeSerialization, value); }

        private IUnitOfWork db;

        public MainWindowViewModel()
        {
            AddCsvFileCommand = new RelayCommand(OnAddCsvFileCommand, CanAddCsvFileCommand);
            ExportExecuteCommand = new RelayCommand(OnExportExecuteCommand, CanExportExecuteCommand);
            ExportOptionWindowCommand = new RelayCommand(OnExportOptionWindowCommandCommand, CanExportOptionWindowCommandCommand);
            CleanDbCommand = new RelayCommand(OnCleanDbCommand, CanCleanDbCommand);
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommand, CanCloseApplicationCommand);

            db = new UnitOfWork();
            UpdateAllRecordsView();
        }

        #region Commands

        #region AddCsvFileToDb

        public ICommand AddCsvFileCommand { get; }

        public bool CanAddCsvFileCommand(object p) => true;
        public void OnAddCsvFileCommand(object p)
        {
            //string CSVFilePath = Path.GetFullPath("D:\\Program_works\\other\\Starter\\Starter\\DataTest.csv");
            CsvDeserialization<Record> csv = new CsvDeserialization<Record>();
            var temp = csv.Deserialization("D:\\Program_works\\other\\Starter\\Starter\\DataTest.csv");
            db.Records.BulkSave(temp);
            db.Save();
            try
            {
                string ReadCSV = File.ReadAllText(OpenFileDialog());

                DataTable tblcsv = new DataTable();
                tblcsv.Columns.Add("Date");
                tblcsv.Columns.Add("Name");
                tblcsv.Columns.Add("SecondName");
                tblcsv.Columns.Add("Patronymic");
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
                DataWorker.InsertCSVRecords(tblcsv);
                UpdateAllRecordsView();

            }
            catch
            {

            }
        }
        public string OpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = "C:\\" };
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            dialog.ShowDialog();

            return dialog.FileName;
        }
        #endregion

        #region ExportExecute
        public ICommand ExportExecuteCommand { get; }

        private bool CanExportExecuteCommand(object arg) => true;

        private void OnExportExecuteCommand(object obj)
        {
            string dialogFilter = "";
            switch (TypeSerialization)
            {
                case "Xml":
                    dialogFilter = "XML-File | *.xml";
                    break;
                case "Excel":
                    dialogFilter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    break;
                default:
                    break;
            }

            var answ = SaveFileDialog(dialogFilter);
            if (!string.IsNullOrEmpty(answ))
            {
                ManagerSerialization<Record> managerSerialization = new ManagerSerialization<Record>(TypeSerialization);
                managerSerialization.Export(answ, DataWorker.SelectRecordLINQ(SampleExport));
            }
        }

        private string SaveFileDialog(string filter)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.ShowDialog();

            return dialog.FileName;
        }
        #endregion

        #region ExportOptionWindow
        public ICommand ExportOptionWindowCommand { get; }

        private bool CanExportOptionWindowCommandCommand(object arg) => true;

        private void OnExportOptionWindowCommandCommand(object obj)
        {
            OpenExportSettingsWindowMethod();
        }
        #endregion

        #region CleanDb
        public ICommand CleanDbCommand { get; }

        public bool CanCleanDbCommand(object p) => true;
        public void OnCleanDbCommand(object p)
        {
            db.Records.RemoveAll();
            db.Save();
            //DataWorker.DeleteAllRecords();
            UpdateAllRecordsView();
        }
        #endregion

        #region Close
        public ICommand CloseApplicationCommand { get; }

        public bool CanCloseApplicationCommand(object p) => true;
        public void OnCloseApplicationCommand(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #endregion

        #region METHODS TO OPEN WINDOW
        //методы открытия окон
        private void OpenExportSettingsWindowMethod()
        {
            ExportSettingsWindow newExportSettingsWindow = new ExportSettingsWindow();
            SetCenterPositionAndOpen(newExportSettingsWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion
        private void UpdateAllRecordsView()
        {
            AllRecords = db.Records.GetAll();
        }
    }
}
