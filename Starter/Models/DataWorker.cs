using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Starter.Models.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models
{
    public static class DataWorker
    {
        public async static Task<List<Record>> SelectRecordLINQ(List<Record> records)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (records != null && records.Count != 0)
                    return (from dbRecord in await db.Records.ToListAsync()
                            from record in records
                            where dbRecord.Equals(record)
                            select dbRecord).ToList();
                else
                    return (from dbRecord in await db.Records.ToListAsync()
                            select dbRecord).ToList();
            }
        }
    }
}
