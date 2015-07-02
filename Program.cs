using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ConvertXmlObjectWithSerialization
{
    /// <summary>
    /// This is small demo of converting XML -> object and object -> XML with Serialization library
    /// We assume that XML is validated (see my demo of XML validation) and do not have major errors
    /// 
    /// database use type "bytearray", so I added conversion object <--> bytearray
    /// 
    /// Serge Klokov 2015
    /// 
    /// Note: XML file have property "Copy to output directory" set to "Copy if newer"
    /// De-serialization of lists usually works fine with XML like in PhoneBookWide.xml
    /// But we will go harder way and de-serialize more condensed version from PhoneBook.xml
    /// Compare these two files and see the difference
    /// PhoneBookRecreated.xml is final after our pipline:
    /// xml->de-serialization->object->serialization->xml
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

            // ********** object to byte array *********
            var byteArray = ObjToByteArray(phoneBook);
            PhoneBook phoneBook2 = ByteArrayToObj(byteArray);

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

        // convert to Byte Array in order to load XML to database
        public static byte[] ObjToByteArray(PhoneBook phoneBook)
        {
            var xmlSerializer = new XmlSerializer(typeof(PhoneBook));
            var memoryStream = new MemoryStream();

            // XML header have "utf-8"
            // better use Unicode, so it will be "utf-32"
            //var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF32);
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xmlSerializer.Serialize(xmlTextWriter, phoneBook);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            return memoryStream.ToArray();
        }

        public static PhoneBook ByteArrayToObj(byte[] byteArray)
        {
            // for debug purpose convert byte array to text (use UTF32 for Unicode)
            var xmlText = System.Text.Encoding.UTF8.GetString(byteArray);

            // encoding doesn't matter here
            using (var memoryStream = new MemoryStream(byteArray))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneBook));
                PhoneBook phoneBook = (PhoneBook)xmlSerializer.Deserialize(memoryStream);
                return phoneBook;
            }
        }

        private static void PrintPhoneBook(PhoneBook phoneBook)
        {
            foreach (var person in phoneBook.People)
            {
                Console.WriteLine("{0} , active = {1}", person.Name, person.Active);

                foreach (var phone in person.Phones)
                    Console.Write("    {0}:{1}", phone.Type.ToString(), phone.Number);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
