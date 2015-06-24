using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ConvertXmlObjectWithSerialization
{
    /// <summary>
    /// This is small demo of converting XML -> object and object -> XML with Serialization library
    /// We assume that XML is validated (see my demo of XML validation) and do not have major errors
    /// Serge Klokov 2015
    /// 
    /// Note: XML file have property "Copy to output directory" set to "Copy if newer"
    /// </summary>    
    class Program
    {
        static void Main(string[] args)
        {
            // convert XML to Object
            var phoneBook = ConvertXmlToObject();

            // print it
            PrintPhoneBook(phoneBook);

            ConvertObjectToXml(phoneBook);

            Console.ReadKey();
        }

        private static PhoneBook ConvertXmlToObject()
        {
            using (FileStream xmlFileStream = new FileStream("XML/PhoneBook.xml", FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneBook));
                PhoneBook phoneBook = (PhoneBook)xmlSerializer.Deserialize(xmlFileStream);
                 
                PrintPhoneBook(phoneBook);

                return phoneBook;
            }
        }

        private static void ConvertObjectToXml(PhoneBook phoneBook)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneBook)); // it's better to use Form.GetType() for generics

            // save to file on the hard drive
            StreamWriter streamWriter = new StreamWriter("XML/PhoneBookRecreated.xml");
            xmlSerializer.Serialize(streamWriter, phoneBook);

            // example how to convert to string, instead of file (for debug purposes)
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, phoneBook);
                string s = stringWriter.ToString();
            }
        }

        private static void PrintPhoneBook(PhoneBook phoneBook)
        {
            //foreach (var person in phoneBook.People)
            //{
            //    Console.WriteLine("{0} , active = {1}", person.Name, person.Active);

            //    foreach (var phone in person.Phones)
            //    {
            //        Console.Write("    {0}:{1}", phone.Type.ToString(), phone.Number);
            //    }

            //    Console.WriteLine();
            //    Console.WriteLine();
            //}
        }
    }
}
