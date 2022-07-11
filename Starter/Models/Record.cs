using System;

namespace Starter.Models
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

    }
}
