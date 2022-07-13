using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Models.Data;
using Starter.Models;

namespace Starter.Commands
{
    public static class DataWorker
    {
        public static List<Record> GetAllRecords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Records.ToList();
            }
        }

        public static void DeleteAllRecords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var entity in db.Records)
                    db.Records.Remove(entity);
                db.SaveChanges();
            }
        }

        public static void InsertCSVRecords(DataTable csvdt)
        {
            DeleteAllRecords();

            var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=starter1;Trusted_Connection=True;");   
            SqlBulkCopy objbulk = new SqlBulkCopy(con); 

            objbulk.DestinationTableName = "Records";  
            objbulk.ColumnMappings.Add("Date", "Date");
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("SecondName", "SecondName");
            objbulk.ColumnMappings.Add("Patranomic", "Patranomic");
            objbulk.ColumnMappings.Add("City", "City");
            objbulk.ColumnMappings.Add("Country", "Country");

            con.Open();
            objbulk.WriteToServer(csvdt);
            con.Close();
        }
    }
}
