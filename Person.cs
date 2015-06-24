using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConvertXmlObjectWithSerialization
{
    public class Person
    {
        [XmlAttribute("id")]
        public int ID { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set;}

        //[XmlAttribute("active")]
        //public bool Active { get; set; }  // it's active unless attribute active="false" exists

        //[XmlArray]
        //[XmlArrayItem(ElementName = "Phone")]
        //public List<Phone> Phones { get; set; }
    }
}
