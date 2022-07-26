﻿using System.Windows;
using Starter.Commands;
using Starter.ViewModels.Base;
using System.Windows.Input;
using System.Data;
using System.IO;
using Microsoft.Win32;
using Starter.Models;
using System.Collections.Generic;
using System;
using Starter.Views;
using Starter.Serialization;

namespace Starter.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private List<Record> allRecords = DataWorker.GetAllRecords();
        public List<Record> AllRecords { get => allRecords; set => Set(ref allRecords, value); }

        private List<Record> sampleExport;
        public List<Record> SampleExport { get => sampleExport; set => Set(ref sampleExport, value); }

        private string typeSerialization;
        public string TypeSerialization { get => typeSerialization; set => Set(ref typeSerialization, value); }

        public MainWindowViewModel()
        {
            sampleExport = new List<Record>();
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommand, CanCloseApplicationCommand);
            AddCsvFileCommand = new RelayCommand(OnAddCsvFileCommand, CanAddCsvFileCommand);
            CleanDbCommand = new RelayCommand(OnCleanDbCommand, CanCleanDbCommand);
            ExportExcelCommand = new RelayCommand(OnExportExcelCommand, CanExportExcelCommand);
            ExportXmlCommand = new RelayCommand(OnExportXmlCommand, CanExportXmlCommand);
            ExportOptionWindowCommand = new RelayCommand(OnExportOptionWindowCommandCommand, CanExportOptionWindowCommandCommand);
            ExportExecuteCommand = new RelayCommand(OnExportExecuteCommand, CanExportExecuteCommand);
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
            //string CSVFilePath = Path.GetFullPath("D:\\Program_works\\other\\Starter\\Starter\\DataTest.csv");
            try
            {
                string ReadCSV = File.ReadAllText(OpenFileDialog());

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

        #region CleanDb
        public ICommand CleanDbCommand { get; }

        public bool CanCleanDbCommand(object p) => true;
        public void OnCleanDbCommand(object p)
        {
            DataWorker.DeleteAllRecords();
            UpdateAllRecordsView();
        }
        #endregion

        #region ExportExcel

        public ICommand ExportExcelCommand { get; }
        public bool CanExportExcelCommand(object p) => true;
        public void OnExportExcelCommand(object p)
        {
            var answ = SaveFileDialog("Excel Files|*.xls;*.xlsx;*.xlsm");
            if (!string.IsNullOrEmpty(answ))
            {
                ManagerSerialization<Record> tr = new ManagerSerialization<Record>("Excel");
                tr.Export(answ, allRecords);
            }
            //OpenExportSettingsWindowMethod();
        }

        private string SaveFileDialog(string filter)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.ShowDialog();

            return dialog.FileName;
        }
        #endregion

        #region ExportXml

        public ICommand ExportXmlCommand { get; }
        private bool CanExportXmlCommand(object arg) => true;

        private void OnExportXmlCommand(object obj) 
        {
            var answ = SaveFileDialog("XML-File | *.xml");
            XmlSerialization<List<Record>> export = new XmlSerialization<List<Record>>();
                if (!string.IsNullOrEmpty(answ))
                    export.Serialization(answ, AllRecords);
            
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
        #region ExportExecute
        public ICommand ExportExecuteCommand { get; }

        private bool CanExportExecuteCommand(object arg) => true;

        private void OnExportExecuteCommand(object obj)
        {
            string dialogFilter ="";
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
            AllRecords = DataWorker.GetAllRecords();
        }
    }
}
