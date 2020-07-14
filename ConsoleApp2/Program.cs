﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    /// <summary>
    /// 
    /// SCRIVO FILE
    // stringa =>  encode =>  array byte => scrivo file
    /// 
    /// LEGGO FILE
    ///  file  => array byte =>  encode => stringa
    /// </summary>
    class Program
    {
        public static void Main()
        {

            #region linq
            int[] data1 = {1, 2, 3, 4};
            IEnumerable<int> res = from d in data1
                select d;
            Console.WriteLine(string.Join(",",res));

            List<Order> orders = new  List<Order>();
            orders.Add(new Order()
            {
                OrderLines = new List<OrderLine>()
                {
                   new OrderLine()
                   {
                       Amount = 1,
                       Product = new Product()
                       {
                           Des = "pippo",
                           Price = 10
                           
                       }
                   }
                }
            });
            orders.Add(new Order()
            {
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Amount = 2,
                        Product = new Product()
                        {
                            Des = "pluto",
                            Price = 20

                        }
                    }
                }
            });

            //grouping and projectons
            var res1 = from o in orders
                from l in o.OrderLines
                group l by l.Product
                into p
                select new
                {
                    Product = p.Key,
                    Amount = p.Sum(x => x.Amount)
                };

            int[] datax = { 1, 2, 3, 4 };
            int[] datay = { 1, 6, 3, 5 };

            IEnumerable<int> rr = from x in datax
                join s2 in datay
                    on x equals s2
                select x;

            Console.WriteLine(string.Join(",", rr));// 1, 3
            #endregion

            #region file
            // Get the current directory.
            //C:\Users\rob\source\repos\Exam\ConsoleApp2\bin\Debug
            //C:\Users\rob\source\repos\Exam\ConsoleApp2\file
            //string p = @"C:\Users\rob\Documents\temp\ciccio.txt";
            //string path1 = Path.GetFullPath(p);
            string pathz = Directory.GetCurrentDirectory();
            string path1 = Path.Combine(pathz, "ciccio.txt");
            var fi1 = new FileInfo(path1);

            // Create a file to write to.
            using (StreamWriter sw = fi1.CreateText())
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }

            // Open the file to read from.
            using (StreamReader sr = fi1.OpenText())
            {
                var s2 = "";
                while ((s2 = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s2);
                }
            }

            //string p = @"C:\Users\rob\Documents\temp\ciccio.txt";
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\rob\Documents\temp");
            try
            {
                // Determine whether the directory exists.
                if (di.Exists)
                {
                    // Indicate that the directory already exists.
                    Console.WriteLine("That path exists already.");
                }
                else
                {
                    // Try to create the directory.
                    di.Create();
                    Console.WriteLine("The directory was created successfully.");
                }

                // Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            //string sourceDirectory = @"C:\current";
            //string archiveDirectory = @"C:\archive";

            //try
            //{
            //    var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.txt");

            //    foreach (string currentFile in txtFiles)
            //    {
            //        string fileName = currentFile.Substring(sourceDirectory.Length + 1);
            //        Directory.Move(currentFile, Path.Combine(archiveDirectory, fileName));
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            string path = @"C:\Users\rob\Documents\temp\ciccio.txt";


            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Create the file.
            using (FileStream fs1 = File.Create(path))
            {
                //byte che servono , da dove , lungo
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text");
                fs1.Write(info, 0, info.Length);

                byte[] info1 = new UTF8Encoding(true).GetBytes("This is some more text,");
                fs1.Write(info1, 0, info1.Length);


                byte[] info2 = new UTF8Encoding(true).GetBytes("\r\nand this is on a new line");
                fs1.Write(info2, 0, info2.Length);
            }

            //Open the stream and read it back.
            using (FileStream fs1 = File.OpenRead(path))
            {
                byte[] b1 = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                //byte che servono , da dove , lungo
                while (fs1.Read(b1, 0, b1.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b1));
                }
            }


            //LEGGO FILE E SCRIVO SU CONSOLE
            using (FileStream fs2 = File.OpenRead(path))
            {
                //creo array byte per contenere grandezza file
                byte[] data = new byte[fs2.Length];

                //per tutta la grandezza del file alimento array di byte con i byte del file
                for (int j = 0; j < fs2.Length; j++)
                {
                    data[j] = (byte)fs2.ReadByte();
                }

                //alla fine devo fare ENCODING da byte a stringa con il formato utf8
                Console.WriteLine(Encoding.UTF8.GetString(data));

            }

            Console.WriteLine(ReadAll(path));
            #endregion

            #region xml

            /*
             * <?xml version="1.0" encoding="utf-16"?>
              <People>
                <Person firstname="J0n" lastname="izzo">
                  <Details>
                   <EmailAddress>j0n@ciccio.it</EmailAddress>
                  </Details>
                </Person>
             </People>
             *
             */
            StringWriter mioxml = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(mioxml, new XmlWriterSettings() {Indent = true}))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("People");
                xmlWriter.WriteStartElement("Person");
                xmlWriter.WriteAttributeString("firstname","J0n");
                xmlWriter.WriteAttributeString("lastname", "izzo");
                xmlWriter.WriteStartElement("Details");
                xmlWriter.WriteElementString("EmailAddress","j0n@ciccio.it");
                //xmlWriter.WriteEndElement();non servono ad un cazzo li chiude da se
                //xmlWriter.WriteEndElement();
                xmlWriter.Flush();
            }

            using (StringReader stringReader = new StringReader(mioxml.ToString()))
            {
                using (XmlReader xmlReader =
                    XmlReader.Create(stringReader, new XmlReaderSettings() {IgnoreWhitespace = true}))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement("People");

                    string firstname = xmlReader.GetAttribute("firstname");
                    string lastname = xmlReader.GetAttribute("lastname");
                    Console.WriteLine($"{firstname}, {lastname}");

                    xmlReader.ReadStartElement("Person");
                    xmlReader.ReadStartElement("Details");

                    string email = xmlReader.ReadString();
                    Console.WriteLine($"{email}");
                }
            }
            #endregion

            #region xmldocument
            XmlDocument doc = new XmlDocument();
            doc.Load("test.xml");
            //doc.Load(mioxml.ToString());

            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;

            //nodeList = root.SelectNodes("descendant::book[author/last-name='Austen']");
            nodeList = doc.GetElementsByTagName("Person", "");
            //Change the price on the books.
            foreach (XmlNode node in nodeList)
            {
                //node.LastChild.InnerText = "15.95";
                var first = node.Attributes["firstname"].Value;
                Console.WriteLine(first);
            }

            //Creo
            XmlNode newnode = doc.CreateNode(XmlNodeType.Element, "Person", "");
            XmlAttribute fAttribute = doc.CreateAttribute("lastname");
            fAttribute.Value = "izzooo";
            newnode.Attributes.Append(fAttribute);
            doc.DocumentElement.AppendChild(newnode);

            doc.Save(Console.Out);

            #endregion

            #region xdocument
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
            srcTree.Save("test1.xml");

            string strXML =
                @"<?xml version=""1.0"" encoding=""utf-16"" ?>
                <People>
                  <Person firstname=""J0n"" lastname=""izzo"">
                   <Details>
                       <EmailAddress>j0n@ciccio.it</EmailAddress>
                   </Details>
                  </Person>
                  <Person firstname=""pippo"" lastname=""pluto"">
                   <Details>
                       <EmailAddress>pippo@pluto.it</EmailAddress>
                   </Details>
                  </Person>
                </People>";

            XDocument docxml = XDocument.Parse(strXML);
            var persons = from p in docxml.Descendants("Person")
                select (string)p.Attribute("firstname") +
                                            " " + (string) p.Attribute("lastname");
            foreach (string item in persons)
            {
               Console.WriteLine(item);
            }

            #endregion

            #region serialization
            PersonSer obj = new PersonSer();

            //BINARIO
            //scrivo binario
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"testbin.bin", FileMode.Create);
            formatter.Serialize(stream, obj);
            stream.Close();

            //leggo binario
            stream = new FileStream(@"testbin.bin", FileMode.Open);
            PersonSer objnew = (PersonSer)formatter.Deserialize(stream);

            Console.WriteLine("BINARY");
            Console.WriteLine(objnew.ID);
            Console.WriteLine(objnew.Name);
            Console.WriteLine(objnew.age);

            //XML
            Console.WriteLine("   SERIALIZATION XML  ");

            XmlSerializer serializer = new XmlSerializer(typeof(PersonSer1));
            String xml1;
            using (StringWriter stringWriter = new StringWriter())
            {
                PersonSer1 p = new PersonSer1();
                p.ID = 1;
                p.Name = "j0n";
                p.age = 43;
                serializer.Serialize(stringWriter, p);
                xml1 = stringWriter.ToString();
            }
            Console.WriteLine(xml1);

            //deserializzo
            using (StringReader streamReader = new StringReader(xml1))
            {
                PersonSer1 p = (PersonSer1)serializer.Deserialize(streamReader);
                Console.WriteLine(p.Name);
            }

            //serializzo
            XmlSerializer serializer1 = new XmlSerializer(typeof(OrderSer), new Type[] { typeof(VipOrderSer) });
            String xml;
            using (StringWriter stringWriter = new StringWriter())
            {
                var order = CreateOrder();
                serializer1.Serialize(stringWriter, order);
                xml = stringWriter.ToString();
            }
            Console.WriteLine("ESEMPIO ORDINE");
            Console.WriteLine("");
            Console.WriteLine(xml);

            //deserializzo
            using (StringReader streamReader = new StringReader(xml))
            {
                OrderSer o = (OrderSer)serializer1.Deserialize(streamReader);
                Console.WriteLine("SECURE");
                Console.WriteLine(o.secure);
            }


            #endregion
           

            #region xml serialize override
            //serialize start
            XmlAttributes attrs = new XmlAttributes();

            XmlElementAttribute attr = new XmlElementAttribute();
            attr.ElementName = "Brass";
            attr.Type = typeof(Brass);

            attrs.XmlElements.Add(attr);

            XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();

            attrOverrides.Add(typeof(Orchestra), "Instruments", attrs);

            XmlSerializer s =
                new XmlSerializer(typeof(Orchestra), attrOverrides);

            TextWriter writer2 = new StreamWriter("override.xml");
            Orchestra band = new Orchestra();
            Brass i = new Brass();
            i.Name = "Trumpet";
            i.IsValved = true;
            Instrument[] myInstruments = { i };
            band.Instruments = myInstruments;
            s.Serialize(writer2, band);
            writer2.Close();
            //serialize end


            //deserialize start
            XmlAttributeOverrides attover =
                new XmlAttributeOverrides();
            XmlAttributes attrs1 = new XmlAttributes();

            XmlElementAttribute attr1 = new XmlElementAttribute();
            attr1.ElementName = "Brass";
            attr1.Type = typeof(Brass);

            attrs1.XmlElements.Add(attr1);

            attover.Add(typeof(Orchestra), "Instruments", attrs1);

            XmlSerializer s1 =
                new XmlSerializer(typeof(Orchestra), attover);

            FileStream fs = new FileStream("override.xml", FileMode.Open);
            Orchestra band1 = (Orchestra)s1.Deserialize(fs);
            Console.WriteLine("Brass:");

            Brass b;
            foreach (Instrument z in band1.Instruments)
            {
                b = (Brass)z;
                Console.WriteLine(
                    b.Name + "\n" +
                    b.IsValved);
            }
            //deserialize end
            #endregion


            #region xmlserialize override XmlAttributeAttribute
            XmlSerializer mySerializer1;
            TextWriter writer1;

            XmlAttributeOverrides myXmlAttributeOverrides1 =
                new XmlAttributeOverrides();
            XmlAttributes myXmlAttributes1 = new XmlAttributes();

          
            XmlAttributeAttribute myXmlAttributeAttribute1 =
                new XmlAttributeAttribute();
            myXmlAttributeAttribute1.AttributeName = "Name";
            myXmlAttributes1.XmlAttribute = myXmlAttributeAttribute1;

            myXmlAttributeOverrides1.Add(typeof(Student), "StudentName",
                myXmlAttributes1);

            // Create the XmlSerializer.
            mySerializer1 = new XmlSerializer(typeof(Student),
                myXmlAttributeOverrides1);

            writer1 = new StreamWriter("student.xml");

            // Create an instance of the class that will be serialized.
            Student myStudent = new Student();

            // Set the Name property, which will be generated as an XML attribute. 
            myStudent.StudentName = "James";
            myStudent.StudentNumber = 1;
            // Serialize the class, and close the TextWriter.
            mySerializer1.Serialize(writer1, myStudent);
            writer1.Close();

            #endregion
        }

        public static OrderSer CreateOrder()
        {
            Product p = new Product()
            {
                Des = "des",
                Price = 50
            };
            Product p1 = new Product()
            {
                Des = "des1",
                Price = 40
            };
            OrderSer order = new VipOrderSer()
            {
                decs = "viporde",
                secure = 111111111,
                code = "XXXXYYYYYY",
                OrderLines = new List<OrderLineSer>()
                {
                    new OrderLineSer()
                    {
                        Product = p,
                        Amount = 11
                    },
                    new OrderLineSer()
                    {
                        Product = p1,
                        Amount = 10
                    }
                }
            };
            return order;
        }

        public static string ReadAll(string path)
        {
            if (File.Exists(path))
            {
               return  File.ReadAllText(path);
            }
            return "";
        }
      
    }

    public class Orchestra
    {
        public Instrument[] Instruments;
    }

    public class Instrument
    {
        public string Name;
    }

    public class Brass : Instrument
    {
        public bool IsValved;
    }

    [Serializable]
    public class PersonSer
    {
        public int ID;
        public String Name;

        [NonSerialized]
        public int age;

        public PersonSer()
        {
            ID = 1;
            Name = "Roby";
            age = 43;
        }
       

        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext con)
        {
            Console.WriteLine("OnSerializingMethod");
        }
    }

    [Serializable]
    public class PersonSer1
    {
        public int ID;
        public String Name;

        [NonSerialized]
        public int age;
    }


    [Serializable]
    public class OrderSer
    {
        [XmlIgnore]
        public String code { get; set; }

        public int secure;

        [XmlArray("Lines")]
        [XmlArrayItem("OrderLine")]
        public List<OrderLineSer> OrderLines { get; set; }
    }

    [Serializable]
    public class VipOrderSer :OrderSer
    {
        public String decs { get; set; }
       
    }

    [Serializable]
    public class OrderLineSer
    {
        [XmlAttribute]
        public int Amount { get; set; }

        [XmlAttribute]
        public int ID { get; set; }

        [XmlElement("Orderproduct")]
        public Product Product { get; set; }
    }

    [Serializable]
    public class ProductSer
    {
        [XmlAttribute]
        public decimal Price { get; set; }
    }


    public class Product
    {
        public string Des { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        public List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
        public int Amount { get; set; }
        public Product Product { get; set; }
    }

    public class Student
    {
        public string StudentName;
        public int StudentNumber;
    }

    public class Book
    {
        public string BookName;
        public int BookNumber;
    }
}
