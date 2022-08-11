using Microsoft.Win32;
using Starter.Commands;
using Starter.Deserialization;
using Starter.Deserialization.Factory;
using Starter.Models;
using Starter.Models.Repositories;
using Starter.Serialization;
using Starter.Serialization.Factory;
using Starter.ViewModels.Base;
using Starter.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Ioc = Microsoft.Toolkit.Mvvm.DependencyInjection.Ioc;

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

        private readonly IUnitOfWork _db;
        private readonly ISerializatorFactory _serializatorFactory;
        private readonly IDeserializatorFactory _deserializatorFactory;

        public MainWindowViewModel()
        {
            AddCsvFileCommand = new RelayCommand(OnAddCsvFileCommand, CanAddCsvFileCommand);
            ExportExecuteCommand = new RelayCommand(OnExportExecuteCommand, CanExportExecuteCommand);
            ExportOptionWindowCommand = new RelayCommand(OnExportOptionWindowCommandCommand, CanExportOptionWindowCommandCommand);
            CleanDbCommand = new RelayCommand(OnCleanDbCommand, CanCleanDbCommand);
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommand, CanCloseApplicationCommand);

            _db = Ioc.Default.GetService<IUnitOfWork>();
            _serializatorFactory = Ioc.Default.GetService<ISerializatorFactory>();
            _deserializatorFactory = Ioc.Default.GetService<IDeserializatorFactory>();
            UpdateAllRecordsView();
        }

        #region Commands

        #region AddCsvFileToDb

        public ICommand AddCsvFileCommand { get; }

        public bool CanAddCsvFileCommand(object p) => true;
        public void OnAddCsvFileCommand(object p)
        {
            try
            {
                ManagerDeserialization<Record> managerDeserialization = new ManagerDeserialization<Record>("Csv", _deserializatorFactory);
                var temp = new List<Record>();
                var answ = OpenFileDialog();
                if (!string.IsNullOrEmpty(answ))
                {
                    temp = managerDeserialization.Import(answ);
                    _db.Records.BulkSave(temp);
                    _db.Save();

                    UpdateAllRecordsView();
                }
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
                ManagerSerialization<Record> managerSerialization = new ManagerSerialization<Record>(TypeSerialization, _serializatorFactory);
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
            _db.Records.RemoveAll();
            _db.Save();
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
            AllRecords = _db.Records.GetAll();
        }
    }
}
