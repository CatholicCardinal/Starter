using Starter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
