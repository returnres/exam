using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp5Xml
{
    class Program
    {
        static void Main(string[] args)
        {

            #region xdocumnt linq to xml
            
            //creation
            //v1
            XDocument srcTree = new XDocument(
                new XComment("This is a comment"),
                new XElement("Root",
                    new XElement("Child1", "data1"),
                    new XElement("Child2", "data2"),
                    new XElement("Child3", "data3"),
                    new XElement("Child2", "data4"),
                    new XElement("Info5", "info5"),
                    new XElement("Info6", "info6"),
                    new XElement("Info7", "info7"),
                    new XElement("Info8", "info8")
                )
                //,new XAttribute("MyAtt",42)
            );
            srcTree.Save("testx1.xml");

            //v2
            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),

                new XComment("Creating an XML Tree using LINQ to XML"),

                new XElement("Students",

                    from student in Student.GetAllStudents()
                    select new XElement("Student", new XAttribute("Id", student.Id),
                        new XElement("Name", student.Name),
                        new XElement("Gender", student.Gender),
                        new XElement("TotalMarks", student.TotalMarks))
                ));

            xmlDocument.Save(@"testx2.xml");



            //add
            XDocument xmlDocument2 = XDocument.Load(@"testx2.xml");

            xmlDocument2.Element("Students").Add(
                new XElement("Student", new XAttribute("Id", 105),
                    new XElement("Name", "Todd"),
                    new XElement("Gender", "Male"),
                    new XElement("TotalMarks", 980)
                ));

            xmlDocument2.Save(@"testx2.xml");


            xmlDocument2.Element("Students")
                .Elements("Student")
                .Where(x => x.Attribute("Id").Value == "103").FirstOrDefault()
                .AddBeforeSelf(
                    new XElement("Student", new XAttribute("Id", 106),
                        new XElement("Name", "Todd"),
                        new XElement("Gender", "Male"),
                        new XElement("TotalMarks", 980)));


            //update
            XDocument xmlDocument3 = XDocument.Load(@"testx2.xml");

            xmlDocument3.Element("Students")
                .Elements("Student")
                .Where(x => x.Attribute("Id").Value == "105").FirstOrDefault()
                .SetElementValue("TotalMarks", 999);

            xmlDocument3.Save(@"testx2.xml");

            XDocument xmlDocumen4 = XDocument.Load(@"testx2.xml");

            xmlDocumen4.Element("Students")
                .Elements("Student")
                .Where(x => x.Attribute("Id").Value == "105")
                .Select(x => x.Element("TotalMarks")).FirstOrDefault().SetValue(999);

            xmlDocumen4.Save(@"testx2.xml");


            //query
            IEnumerable<string> names = from student in XDocument
                    .Load(@"testx2.xml")
                    .Descendants("Student")
                where (int)student.Element("TotalMarks") > 800
                orderby (int)student.Element("TotalMarks") descending
                select student.Element("Name").Value;

            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
            #endregion

            #region xmldocumnt modifichi e navighi

            XmlDocument doc = new XmlDocument();
            doc.Load("testxml.xml");

            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;

            nodeList = doc.GetElementsByTagName("Person", "");

            //Change the price on the books.
            foreach (XmlNode node in nodeList)
            {
                var first = node.Attributes["firstname"].Value;
                Console.WriteLine(first);
                if (root.HasChildNodes)
                {
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        Console.WriteLine(root.ChildNodes[i].InnerText);
                    }
                }
            }
            XmlElement bookElement = (XmlElement)nodeList[1];

            // Get the attributes of a book.
            bookElement.SetAttribute("firstname", "ricky");

            // Get the values of child elements of a book.
            //bookElement["EmailAddress"].InnerText = "antani@sblinda";

            //Creo
            XmlNode newnode = doc.CreateNode(XmlNodeType.Element, "Person", "");
            XmlAttribute fAttribute = doc.CreateAttribute("lastname");
            fAttribute.Value = "izzooo";
            newnode.Attributes.Append(fAttribute);
            doc.DocumentElement.AppendChild(newnode);

            doc.Save(Console.Out);
            #endregion

            #region xmlreader  crei / leggi xml
            //MODO1 
            XmlReader xmlReader = XmlReader.Create("test.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "book"))
                {
                    if (xmlReader.HasAttributes)
                        Console.WriteLine(xmlReader.GetAttribute("genre"));
                }

                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "title"))
                {
                    Console.WriteLine($"title: {xmlReader.ReadElementContentAsString()}");
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

            #endregion
           
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int TotalMarks { get; set; }

        public static Student[] GetAllStudents()
        {
            Student[] students = new Student[4];

            students[0] = new Student
            {
                Id = 101,
                Name = "Mark",
                Gender = "Male",
                TotalMarks = 800
            };
            students[1] = new Student
            {
                Id = 102,
                Name = "Rosy",
                Gender = "Female",
                TotalMarks = 900
            };
            students[2] = new Student
            {
                Id = 103,
                Name = "Pam",
                Gender = "Female",
                TotalMarks = 850
            };
            students[3] = new Student
            {
                Id = 104,
                Name = "John",
                Gender = "Male",
                TotalMarks = 950
            };

            return students;
        }
    }
}