﻿using System.Windows;
using Starter.Commands;
using Starter.ViewModels.Base;
using System.Windows.Input;
using System.Data;
using System.IO;
using Microsoft.Win32;
using Starter.Models;
using System.Collections.Generic;

namespace Starter.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private List<Record> allRecords = DataWorker.GetAllRecords();
        public List<Record> AllRecords { get => allRecords; set => Set(ref allRecords, value); }
        
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommand, CanCloseApplicationCommand);
            AddCsvFileCommand = new RelayCommand(OnAddCsvFileCommand, CanAddCsvFileCommand);
            CleanDbCommand = new RelayCommand(OnCleanDbCommand, CanCleanDbCommand);
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
            DataTable tblcsv = new DataTable();
            tblcsv.Columns.Add("Date");
            tblcsv.Columns.Add("Name");
            tblcsv.Columns.Add("SecondName");
            tblcsv.Columns.Add("Patranomic");
            tblcsv.Columns.Add("City");
            tblcsv.Columns.Add("Country");


            //string CSVFilePath = Path.GetFullPath("D:\\Program_works\\other\\Starter\\Starter\\DataTest.csv");
            string ReadCSV = File.ReadAllText(OpenFileDialog());

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
        public string OpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = "C:\\" };
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

        #endregion

        private void UpdateAllRecordsView()
        {
            AllRecords = DataWorker.GetAllRecords();
        }
    }
}
