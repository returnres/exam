using System;
using System.Xml.XPath;

namespace ConsoleApp1
{
    internal class MiaclasseProva :IMiaclasseProva
    {
        private int nome;
        public string Surname { get; set; }
        public string MioMetodo(int a)
        {
            return "";
        }

        private void MetodoProvato(int a)
        {
            
        }
    }

    internal interface IMiaclasseProva
    {
        string MioMetodo(int a);
    }

    public interface IIbabbo
    {
        void MioMetodo();
    }

    public class Ibabbo : IIbabbo
    {
        public void MioMetodo()
        {
            Console.Write("");
        }
    }

    public class Figliolo : Ibabbo
    {
        
    }

    public class Ciccio : ICiccio ,ICiccio1,IPluto
    {
        void ICiccio.Prova()
        {
            Console.Write("ICiccio.Prova()");
        }

        void ICiccio1.Prova()
        {
            Console.Write("ICiccio1.Prova()");
        }

        public void Prova()
        {
            Console.Write("metodo");
        }

        public void Nano()
        {
            Console.Write("Nano");
        }
    }

    public interface IPluto
    {
        void Nano();
    }

    public interface ICiccio
    {
        void Prova();
    }

    public interface ICiccio1
    {
        void Prova();

    }
}