using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Models
{
    public class City
    {      
        public int Id { get; set; }
        public string CityName { get; set; }
        public List<Record> Records { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
