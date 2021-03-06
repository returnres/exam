﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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
            /*
             * TextReader
             * Stringreader/StringWriter (xmlreader)
             * StreamReader/StreamWriter
             * 
             * 
             *                  LETTURA SCRITTURA FILE
             * FileStream => Encoding usa File  che ritorna filestream
             * StreamReader/StreamWriter => no encoding usa FileInfo
             * 
             * 
             *                  LETTURA SCRITTURA XML
             * XmlDocument => lento facile non lettua sequanzaile
             * XmlReader => StringReader  veloce lettura sequnziale
             * XDocument => linq to xml per leggere, editare
             * 
             * 
             *                  SERIALIZZAZIONE
             * XmlSerialize => StringWriter/StreamWriter, xmlignore, no priv, setter,serialize
             * DataContractSerializer  =>FileStream DataContract Datamember (senza non serializza) obbligatori (wcf)
             * BynaryFormatter => FileStream, noserialize, priv si
             * DataConntractJsonSerializer => MemoryStream DataContract Datamember (senza non serializza)
             * javascriptserializer
             */

            #region STREAM 

            /*
             * Streams
Uno stream è utile per leggere o scrivere poco alla volta, 
senza dover caricare tutto in memoria. 
Ha 3 funzioni principali: reading, writing e seeking. Non tutti gli streams sono seekable, però.
Un FileStream è quello che ti ritorna quando invochi File.Open o File.Create. 
Potrebbe non essere quello che vuoi, perché il FileStream lavora solo con dati binari.ED è necessario usare 
encoding 

Allora forse ti conviene usare File.OpenText o File.CreateText, che restituiscono uno StreamWriter,
che ti permette di scrivere stringhe in formato UTF8.

using (StreamWriter streamWriter = File.CreateText(path))
{
	string myValue = "MyValue";
	streamWriter.Write(myValue);
}

Se vuoi scrivere file in altri encoding, arrangiati col FileStream. 
Gli passerai i bytes frutto della conversione con Encoding.GetEncoding(“iso-8859-1”).GetBytes.
                                                    DECORATOR PATTERN

Gli stream possono essere combinati secondo il  DECORATOR PATTERN
Ad esempio, usare un FileStream da solo è certamente fattibile ma se lo usi per scrivere un byte alla volta 
non avrai ottime prestazioni perché i disci sono più abili a scrivere grosse quantità di dati alla volta.

Allora puoi aggiungere una funzionalità passandolo come argomento al costruttore di un altro stream, 
che adatta lo stream base a supportare nuove funzionalità, come il buffering. 
Lo StreamWriter, per esempio, permette di scrivere facilmente stringhe su 
un FileStream di base che usa internamente (o che passi nel costruttore).
 * 
             * 
             */
            // filestream base
            using (FileStream fileStream = File.Create("ciccio1.txt")) {
	       // aggiungo la capacità di accumulare bytes in un buffer
	             using (BufferedStream bufferedStream = new BufferedStream(fileStream)) {
		   // creo un writer per scrivere (non è esso stesso uno stream)
		    // magari potevo aggiungere un ulteriore Stream per zippare
		            using (StreamWriter streamWriter = new StreamWriter(bufferedStream)) {
			           streamWriter.WriteLine("A line of text.");
		             }
	              }
              }

            
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

            #region  xml valiate

            string xsdPath = "pippo.xsd";
            string xmlPath = "test.xml";
            XmlReader reader = XmlReader.Create(xmlPath);
            XmlDocument document = new XmlDocument();
            document.Schemas.Add("", xsdPath);
            document.Load(reader);
            var eventHandler = new ValidationEventHandler(ValidationEventHandler);
            document.Validate(eventHandler);
            #endregion

            #region xml XmlWriter XmlReader
           
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
            /*
             * <?xml version="1.0" encoding="utf-16"?>
<People>
  <Person firstname="J0n" lastname="izzo">
    <Details>
      <EmailAddress>j0n@ciccio.it</EmailAddress>
    </Details>
  </Person>
</People>
             */
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
            //SCRIVO
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

            //LEGGO
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

            //BINARIO    field privati vengono serializzati di default va messo nonserialized
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

            //XML NON SERIALIZZA PRIVATI E VA MESSO XMLIGNRE PER NON SERIALIZZARE , (NON NOTSERILIZED NON FUNZIONA
            Console.WriteLine("   SERIALIZATION XML  ");

            XmlSerializer serializer = new XmlSerializer(typeof(PersonSer1));
            String xml1;
            using (StringWriter stringWriter = new StringWriter())
            {
                PersonSer1 p = new PersonSer1();
                p.ID = 1;
                p.Name = "j0n";
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

            XmlAttributeOverrides myXmlAttributeOverrides1 =
                new XmlAttributeOverrides();
            XmlAttributes myXmlAttributes1 = new XmlAttributes();
          
            XmlAttributeAttribute myXmlAttributeAttribute1 =
                new XmlAttributeAttribute();
            myXmlAttributeAttribute1.AttributeName = "Name";
            myXmlAttributes1.XmlAttribute = myXmlAttributeAttribute1;

            myXmlAttributeOverrides1.Add(typeof(Student), "StudentName", myXmlAttributes1);

            var mySerializer1 = new XmlSerializer(typeof(Student),
                myXmlAttributeOverrides1);
            TextWriter writer1 = new StreamWriter("student.xml");

            Student myStudent = new Student();
            myStudent.StudentName = "James";
            myStudent.StudentNumber = 1;
            mySerializer1.Serialize(writer1, myStudent);
            writer1.Close();

            #endregion

            #region DATA CONTRACT
            FilmOrror filmOrror = new FilmOrror("memento",123);
            using (Stream stream1 = new FileStream("data.xml", FileMode.Create))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(FilmOrror));
                dcs.WriteObject(stream1,filmOrror);
            }

            using (Stream stream1 = new FileStream("data.xml", FileMode.Open))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(FilmOrror));
                var resf = (FilmOrror)dcs.ReadObject(stream1);
            }

            #endregion

            #region jsonserialize
            var films = new FilmOrror("rambo", 42);
            films.success = "ok";

            var ms = new MemoryStream();

            // scrivo
            var ser = new DataContractJsonSerializer(typeof(FilmOrror));
            ser.WriteObject(ms, films);
            byte[] json = ms.ToArray();
            ms.Close();

            //leggo
            FilmOrror deserializedUser = new FilmOrror();
            var miojson =Encoding.UTF8.GetString(json, 0, json.Length);
            var ms1 = new MemoryStream(Encoding.UTF8.GetBytes(miojson));
            var ser1 = new DataContractJsonSerializer(deserializedUser.GetType());
             deserializedUser = ser1.ReadObject(ms1) as FilmOrror;
            ms.Close();
            #endregion

            #region javascriptserialize
            var RegisteredUsers = new List<Uomo>();
            RegisteredUsers.Add(new Uomo("rambo",1));
            RegisteredUsers.Add(new Uomo("pippo",1));

            var sssss = new JavaScriptSerializer();
            var serializedResult1 = sssss.Serialize(RegisteredUsers);
            // Produces string value of:
            // [
            //     {"PersonID":1,"Name":"Bryon Hetrick","Registered":true},
            //     {"PersonID":2,"Name":"Nicole Wilcox","Registered":true},
            //     {"PersonID":3,"Name":"Adrian Martinson","Registered":false},
            //     {"PersonID":4,"Name":"Nora Osborn","Registered":false}
            // ]

            var deserializedResult = sssss.Deserialize<List<Uomo>>(serializedResult1);
            // Produces List with 4 Person objects
            #endregion
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error:{0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
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

    [DataContract(Name = "Movie", Namespace = "http://www.contoso.com")]
    public class Film
    {
        [DataMember(Name = "Money")]
        internal decimal Money;

        public Film()
        {
                
        }
    }

    [DataContract(Name = "MovieOrror", Namespace = "http://www.contoso.com")]
    public class FilmOrror : Film
    {
        [DataMember(Name = "CustName",Order = 2)]
        internal string Name;

        [DataMember(Name = "CustID",Order = 1)]
        internal int ID;

        [DataMember(Name = "IE", Order = 1)]
        internal int IE;

        [DataMember(Name = "Success")]
        internal string success;

        public FilmOrror()
        {
                
        }
        public FilmOrror(string newName, int newID)
        {
            Name = newName;
            ID = newID;
        }
    }

    public class   Uomo
    {
        public Uomo()
        {
                
        }
        public Uomo(string name1 ,int eta1)
        {
            name = name1;
            eta = eta1;
        }
    public int eta { get; set; }
    public string name { get; set; }
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
        private int age;

        private int nonsicuro;
        public PersonSer()
        {
            ID = 1;
            Name = "Roby";
            age = 43;
            nonsicuro = 2;
        }


        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerializingMethod");
        }
        [OnSerialized()]
        internal void OnSerializedMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerializedMethod");
        }
        [OnDeserializing()]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            Console.WriteLine("OnDeserializingMethod");
        }
        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Console.WriteLine("OnDeserializedMethod");
        }

    }

    [Serializable]
    public class PersonSer1
    {
        public int ID;
        public String Name;

        [XmlIgnore]
        private int age;

        private string insicuro;

        public PersonSer1()
        {
            age = 43;
            insicuro = "insicuro";
        }
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
        public string name { get; set; }
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
