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

        //[XmlAttribute("type")]
        //public PhoneType Type { get; set; }

        // we need to do this in order to convert "woodden" (not part of the enum) to "unknown" default
        [XmlIgnore]
        private PhoneType _Type;

        [XmlAttribute("type")]
        public string Type
        {
            get { return _Type.ToString(); }
            set { _Type = SergeConversions.GetPhoneType(value); }
        }

        
    }
}
