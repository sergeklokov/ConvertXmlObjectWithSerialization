using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConvertXmlObjectWithSerialization
{
    public class Phone
    {
        [XmlText]
        public string Number { get; set; }

        [XmlAttribute("type")]
        public PhoneType Type { get; set; }
    }
}
