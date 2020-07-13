using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                var s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
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
            using (FileStream fs = File.Create(path))
            {
                //byte che servono , da dove , lungo
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text");
                fs.Write(info, 0, info.Length);

                byte[] info1 = new UTF8Encoding(true).GetBytes("This is some more text,");
                fs.Write(info1, 0, info1.Length);


                byte[] info2 = new UTF8Encoding(true).GetBytes("\r\nand this is on a new line");
                fs.Write(info2, 0, info2.Length);
            }

            //Open the stream and read it back.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                //byte che servono , da dove , lungo
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }


            //LEGGO FILE E SCRIVO SU CONSOLE
            using (FileStream fs = File.OpenRead(path))
            {
                //creo array byte per contenere grandezza file
                byte[] data = new byte[fs.Length];

                //per tutta la grandezza del file alimento array di byte con i byte del file
                for (int i = 0; i < fs.Length; i++)
                {
                    data[i] = (byte)fs.ReadByte();
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
}
