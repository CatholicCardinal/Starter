using Starter.Attributes;
using System;
using System.Xml.Serialization;

namespace Starter.Models
{
    [Serializable]
    public class Record
    {
        [SkipProperty]
        [XmlAttribute("ID")]
        public int Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Record record &&
                   (record.Date == null || Date == record.Date) &&
                   (record.Name == null || Name == record.Name) &&
                   (record.SecondName == null || SecondName == record.SecondName) &&
                   (record.Patronymic == null || Patronymic == record.Patronymic) &&
                   (record.City == null || City == record.City) &&
                   (record.Country == null || Country == record.Country );
        }
    }
}
