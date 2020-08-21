using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp5Xml
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Linq to xml   xdoument, Xelement ,XAttribute
             */
            #region xdocumnt linq to xml => navighi ,crei , edito  

            //creation

            //v0

            XDocument docx = new XDocument(new XElement("body",
                new XElement("level1",
                    new XElement("level2", "text"),
                    new XElement("level2", "other text"))));
            docx.Save("testx0.xml");

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

            /*
             * meno performante   xmldocument , xmldocument ,  XmlElement
             * read the whole thing into memory and build a DOM
             */
            #region xmldocumnt => navighi , crei, edito 


            //v0
            XmlDocument doc2 = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc2.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc2.DocumentElement;
            doc2.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement element1 = doc2.CreateElement(string.Empty, "body", string.Empty);
            doc2.AppendChild(element1);

            XmlElement element2 = doc2.CreateElement(string.Empty, "level1", string.Empty);
            element1.AppendChild(element2);

            XmlElement element3 = doc2.CreateElement(string.Empty, "level2", string.Empty);
            XmlText text1 = doc2.CreateTextNode("text");
            element3.AppendChild(text1);
            element2.AppendChild(element3);

            XmlElement element4 = doc2.CreateElement(string.Empty, "level2", string.Empty);
            XmlText text2 = doc2.CreateTextNode("other text");
            element4.AppendChild(text2);
            element2.AppendChild(element4);

            doc2.Save(@"testxmldocument.xml");


            //v1
            XmlDocument doc = new XmlDocument();
            doc.Load("testxml.xml");

            XmlNodeList nodeList;
            XmlNode root1 = doc.DocumentElement;

            nodeList = doc.GetElementsByTagName("Person", "");

            //Change the price on the books.
            foreach (XmlNode node in nodeList)
            {
                var first = node.Attributes["firstname"].Value;
                Console.WriteLine(first);
                if (root1.HasChildNodes)
                {
                    for (int i = 0; i < root1.ChildNodes.Count; i++)
                    {
                        Console.WriteLine(root1.ChildNodes[i].InnerText);
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


            /*
             * Fast way create read no cache
             * Rappresenta un writer che fornisce un modo veloce, non in cache e di tipo forward-only 
             * per generare flussi o i file contenenti dati XML.
             * XmlReader/Writer are sequential access streams. You will have to read in on one end, 
             * process the stream how you want, and write it out the other end. The advantage is that you don't need to read 
             * the whole thing into memory and build a DOM, which is what you'd get with any XmlDocument-based approach.
             * 
             */
            #region xmlreader =>  crei / navighi xml, NON POSSO EDITARE

            //
            var myreader = XmlReader.Create("products.xml");
            myreader.MoveToContent();
            while (myreader.Read())
            {
                var result = myreader.NodeType;
                //{
                //    XmlNodeType.Element when myreader.Name == "product" => $"{myreader.Name}\n",
                //    XmlNodeType.Element => $"{myreader.Name}: ",
                //    XmlNodeType.Text => $"{myreader.Value}\n",
                //    XmlNodeType.EndElement when myreader.Name == "product" =>  "----------------------\n",
                //    _ => ""
                //};

                Console.Write(result);
            }

            //
            XmlReader readerexample = XmlReader.Create("products.xml");
            readerexample.MoveToContent();

            while (readerexample.MoveToContent() == XmlNodeType.Element && readerexample.LocalName == ChildXmlTag)
            {
                readerexample.ReadStartElement();
                readerexample.MoveToContent();

                //IilParameter p = IilParameter.fromString(readerexample.LocalName);
                //p.ReadXml(readerexample);
                //Parameters.Add(p);
                //readerexample.ReadEndElement();
            }
            readerexample.ReadEndElement();

            //
            XmlReader reader5 = XmlReader.Create("products.xml");
            reader5.MoveToContent();
            var empty = reader5.IsEmptyElement;
            reader5.ReadStartElement();
            if (!empty)
            {
                while (reader5.MoveToContent() == XmlNodeType.Element)
                {
                    //if (reader5.Name == @"ProductName" && !reader5.IsEmptyElement)
                    //{
                    //    ProductName = reader5.ReadElementString();
                    //}
                    //else if (reader5.Name == @"GlyphColor" && !reader5.IsEmptyElement)
                    //{
                    //    GlyphColor = ParseColor(reader5.ReadElementString());
                    //}
                    //else
                    //{
                    //    // consume the bad element and skip to next sibling or the parent end element tag
                    //    reader5.ReadOuterXml();
                    //}
                }
                reader5.MoveToContent();
                reader5.ReadEndElement();
            }



            //MODO0 leggo da uno scrivo da un altro 
            using (FileStream readStream = new FileStream(@"xmlreadertest.xml", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Write))
            {
                using (FileStream writeStream = new FileStream(@"xmlreadertestNEW.xml", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    PostProcess(readStream, writeStream);
                }
            }

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
            var reader1 = XmlReader.Create("test1.xml");
            reader1.ReadToFollowing("book");

            do
            {
                reader1.MoveToFirstAttribute();
                Console.WriteLine($"genre: {reader1.Value}");

                reader1.ReadToFollowing("title");
                Console.WriteLine($"title: {reader1.ReadElementContentAsString()}");

                reader1.ReadToFollowing("author");
                Console.WriteLine($"author: {reader1.ReadElementContentAsString()}");

                reader1.ReadToFollowing("price");
                Console.WriteLine($"price: {reader1.ReadElementContentAsString()}");

                Console.WriteLine("-------------------------");

            } while (reader1.ReadToFollowing("book"));


            //MODO3 
            using (XmlReader readertest = XmlReader.Create("test3.xml"))
            {
                while (readertest.Read())
                {
                    // Only detect start elements.
                    if (readertest.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (readertest.Name)
                        {
                            case "perls":
                                // Detect this element.
                                Console.WriteLine("Start <perls> element.");
                                break;
                            case "article":
                                // Detect this article element.
                                Console.WriteLine("Start <article> element.");
                                // Search for the attribute name on this current node.
                                string attribute = readertest["name"];
                                if (attribute != null)
                                {
                                    Console.WriteLine("  Has attribute name: " + attribute);
                                }
                                // Next read will contain text.
                                if (readertest.Read())
                                {
                                    Console.WriteLine("  Text node: " + readertest.Value.Trim());
                                }
                                break;
                        }//fine switch
                    }//fine if
                }
            }

            #endregion

        }


        public static void PostProcess(Stream inStream, Stream outStream)
        {
            var settings = new XmlWriterSettings() { Indent = true, IndentChars = " " };

            using (var reader = XmlReader.Create(inStream))
            using (var writer = XmlWriter.Create(outStream, settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {

                        //
                        // check if this is the node you want, inject attributes here.
                        //
                        case XmlNodeType.Element:
                            writer.WriteStartElement(reader.Prefix, reader.Name, reader.NamespaceURI);
                            writer.WriteAttributes(reader, true);


                            if (reader.IsEmptyElement)
                            {
                                writer.WriteEndElement();
                            }
                            break;

                        case XmlNodeType.Text:
                            writer.WriteString(reader.Value);
                            break;

                        case XmlNodeType.EndElement:
                            writer.WriteFullEndElement();
                            break;

                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                            writer.WriteProcessingInstruction(reader.Name, reader.Value);
                            break;

                        case XmlNodeType.SignificantWhitespace:
                            writer.WriteWhitespace(reader.Value);
                            break;
                    }
                }
            }
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