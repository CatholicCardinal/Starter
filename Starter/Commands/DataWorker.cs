using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Models.Data;

namespace Starter.Commands
{
    public static class DataWorker
    {
        public static void InsertCSVRecords(DataTable csvdt)
        {
            var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=starter1;Trusted_Connection=True;");
            //creating object of SqlBulkCopy    
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assigning Destination table name    
            objbulk.DestinationTableName = "RecordTests";
            //Mapping Table column    
            objbulk.ColumnMappings.Add("Date", "Date");
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("SecondName", "SecondName");
            objbulk.ColumnMappings.Add("Patranomic", "Patranomic");
            objbulk.ColumnMappings.Add("City", "City");
            objbulk.ColumnMappings.Add("Country", "Country");

            //inserting Datatable Records to DataBase    
            con.Open();
            objbulk.WriteToServer(csvdt);
            con.Close();
        }
    }
}
