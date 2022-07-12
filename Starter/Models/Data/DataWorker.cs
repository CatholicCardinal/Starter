using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Models;
using System.Data.Odbc;

namespace Starter.Models.Data
{
    public static class DataWorker
    {
        public static List<RecordTest> GetAllRecords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.RecordTests.ToList();
            }
        }

        public static List<string> GetAllRecords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.
            }
        }

        public static void InsertCSVRecords(DataTable csvdt, string FileName)
        {
            var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=starter1;Trusted_Connection=True;");

            con.Open();
            if (!IsTableExisting(FileName, con))
            {
                SqlCommand cmd = new SqlCommand(CreateTable(FileName, csvdt).ToString(), con);
                cmd.ExecuteNonQuery();
            }
            con.Close();

            con.Open();
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = FileName.ToString();
            objbulk.ColumnMappings.Add("Date", "Date");
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("SecondName", "SecondName");
            objbulk.ColumnMappings.Add("Patranomic", "Patranomic");
            objbulk.ColumnMappings.Add("City", "City");
            objbulk.ColumnMappings.Add("Country", "Country");

            objbulk.WriteToServer(csvdt);
            con.Close();
        }

        private static bool IsTableExisting(string table, SqlConnection con)
        {
            string command = $"select * from sys.tables";
            using (SqlCommand com = new SqlCommand(command, con))
            {
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0).ToLower() == table.ToLower())
                        return true;
                }
                reader.Close();
            }
            return false;
        }
        private static string CreateTable(string tableName, DataTable table)
        {
            string sqlsc;
            sqlsc = "CREATE TABLE " + tableName + "(\n" + " [Id]  INT  IDENTITY(1,1)  NOT NULL ,";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n [" + table.Columns[i].ColumnName + "] ";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += "," ;
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n PRIMARY KEY(Id))";
        }
    }
}
