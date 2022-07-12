using System.Windows;
using Starter.Commands;
using Starter.ViewModels.Base;
using System.Windows.Input;
using System.Data;
using System.IO;
using Microsoft.Win32;

namespace Starter.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommand, CanCloseApplicationCommand);
            AddCsvFileCommand = new RelayCommand(OnAddCsvFileCommand, CanAddCsvFileCommand);
            OpenFileDialogCommand = new RelayCommand(OnExecuteOpenFileDialog, CanExecuteOpenFileDialog);
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
            string ReadCSV = File.ReadAllText(_selectedPath);

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
        }

        #endregion

        #region OpenFileDialog
        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath ?? "C:\\"; }
            set
            {
                _selectedPath = value;
            }
        }

        private string _defaultPath = "C:\\";
        public static ICommand OpenFileDialogCommand { get; set; }
        public bool CanExecuteOpenFileDialog(object p) => true;
        public void OnExecuteOpenFileDialog(object p)
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;

            OnAddCsvFileCommand(p);
        }
        #endregion

        #endregion
    }
}
