using Starter.Attributes;
using System;
using System.Xml.Serialization;

namespace Starter.Models
{
    [Serializable]
    public abstract class BaseModel
    {
        [SkipProperty]
        [XmlAttribute("ID")]
        public int Id { get; set; }
    }
}
