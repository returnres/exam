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
            //MODO1
            XmlReader xmlReader = XmlReader.Create("test.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "book"))
                {
                    if (xmlReader.HasAttributes)
                        Console.WriteLine(xmlReader.GetAttribute("genre"));
                }
            }

            //MODO2
            var reader = XmlReader.Create("test1.xml");
            reader.ReadToFollowing("book");

            do
            {
                reader.MoveToFirstAttribute();
                Console.WriteLine($"genre: {reader.Value}");

                reader.ReadToFollowing("title");
                Console.WriteLine($"title: {reader.ReadElementContentAsString()}");

                reader.ReadToFollowing("author");
                Console.WriteLine($"author: {reader.ReadElementContentAsString()}");

                reader.ReadToFollowing("price");
                Console.WriteLine($"price: {reader.ReadElementContentAsString()}");

                Console.WriteLine("-------------------------");

            } while (reader.ReadToFollowing("book"));


            //MODO3
            using (XmlReader reader1 = XmlReader.Create("test3.xml"))
            {
                while (reader1.Read())
                {
                    // Only detect start elements.
                    if (reader1.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader1.Name)
                        {
                            case "perls":
                                // Detect this element.
                                Console.WriteLine("Start <perls> element.");
                                break;
                            case "article":
                                // Detect this article element.
                                Console.WriteLine("Start <article> element.");
                                // Search for the attribute name on this current node.
                                string attribute = reader1["name"];
                                if (attribute != null)
                                {
                                    Console.WriteLine("  Has attribute name: " + attribute);
                                }
                                // Next read will contain text.
                                if (reader1.Read())
                                {
                                    Console.WriteLine("  Text node: " + reader1.Value.Trim());
                                }
                                break;
                        }//fine switch
                    }//fine if
                }
            }
        }
    }
}
