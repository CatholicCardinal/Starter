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
        public string Patranomic { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Record record &&
                   (Date == record.Date || record.Date == null) &&
                   (Name == record.Name || record.Name == null) &&
                   (SecondName == record.SecondName || record.SecondName == null) &&
                   (Patranomic == record.Patranomic || record.Patranomic == null) &&
                   (City == record.City || record.City == null) &&
                   (Country == record.Country || record.Country == null);
        }
    }
}
