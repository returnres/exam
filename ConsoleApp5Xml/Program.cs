using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp5Xml
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReader xmlReader = XmlReader.Create("test.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                {
                    if (xmlReader.HasAttributes)
                        Console.WriteLine(xmlReader.GetAttribute("currency") + ": " + xmlReader.GetAttribute("rate"));
                }
            }



            using (XmlReader xmlReader1 = XmlReader.Create("test.xml"))
            {
                xmlReader1.MoveToContent();
                xmlReader1.ReadStartElement("People");

                string firstname = xmlReader1.GetAttribute("firstname");
                string lastname = xmlReader1.GetAttribute("lastname");
                Console.WriteLine($"{firstname}, {lastname}");

                xmlReader1.ReadStartElement("Person");
                xmlReader1.ReadStartElement("Details");

                string email = xmlReader1.ReadString();
                Console.WriteLine($"{email}");
            }
        }
    }
}
