using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConvertXmlObjectWithSerialization
{
    /// <summary>
    /// really it's just root object, 
    /// usually we can ignore it 
    /// </summary>

    [XmlRoot("PhoneBook")]
    public class PhoneBook //: List<Person>
    {
        [XmlAttribute("owner")]
        public string Owner { get; set; }

        // Main data stored in People
        // this would work if we would have element <People></People> around collection "Person" 
        //[XmlArray]
        //[XmlArrayItem(ElementName = "Person")]
        public List<Person> People { get; set; }
    }
}
