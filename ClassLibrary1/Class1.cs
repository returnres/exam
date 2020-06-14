using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //astrazione
    //incapsulamento
    //ereditarietà
    //polimorfismo
    public class Miaclasse : Miaclasse1
    {
        /*memebri :
        * field
        * prop
        * costruttore
        * distruttore
        * costante
        * evento
        * operatore
        * indicizzatore
        * tipo innestato
        */
        private const string pianeta = "terra";

        private readonly DateTime datanascrita;
        protected string nome { get; set; }
        protected internal int anni { get; set; }

        public Miaclasse()
        {
            datanascrita = DateTime.Now;
        }
        //membri
        public string pippo { get; set; }

        public string pluto
        {
            get { return pluto; }

            set { pluto = value; }
        }

        //expression body
        public string papaperino
        {
            get => papaperino;
            set => papaperino = value;
        }
    }

    public class ClassTest1
    {
        //expression body
        //
        public int Somma(int a, int b) => a + b;

        public void Stampa(string str) => Console.WriteLine(str);

        //OVERLOAD FIRMA = NOME +ARGOMENTI INGERSSO
        public int ripetiNumero(int a)
        {
            return a;
        }
        public int ripetiNumero(int a, int b)
        {
            return a;
        }

        //passare parametri per rif
        public int cambiaNumero(ref int a)
        {
            return a;
        }

        //passare + parametri in uscita c#6
        public void potenza(int val, out double p2, out double p3)
        {
            p2 = Math.Pow(val, 2);
            p3 = Math.Pow(val, 3);
        }
        //passare + parametri in uscita c#7
        public void potenza1(int val, out double p2, out double p3)
        {
            p2 = Math.Pow(val, 2);
            p3 = Math.Pow(val, 3);
        }

        public double CalcolaMedia(params double[] array)
        {
            return array.Average();
        }

        public void ParametriNome(string nome, int anni, string nazione = null)
        {

        }
    }

    public class Babbo
    {

    }

    public class Figlio : Babbo
    {

    }

    public interface Imiainterfaccia
    {
    }

    public class Miaclasse1 : Imiainterfaccia
    {

    }

    public enum Mesi
    {
        gennaio = 0,
        febbraio = 1
    }

    public struct Pippo
    {
        public int a;
    }

    //tipi rif class int dele arr obj string dyna
    //tipi val struct enum
    //tipi primitivi sono struct byte short int long bool float double decimal
    public class TestClass
    {
        //props
        public string color { get; set; }

        //filed
        public string hair;

        public void MyMethod()
        {


            //espressione
            int i = 1 + 2;

            //blocco
            unchecked
            {
                byte b1 = 200;
                byte b2 = 100;
                byte somma = (byte)(b1 + b2);
            }

            //underflowexception
            byte min = Byte.MinValue;
            //byte underflow = checked(min - 1);

            /* & and
             * | or
             * ^ xor
             *      
             *      
             */
            object a = null;
            object b = 123;

            //miòò coalescing
            //se è null gli metto un altea cosa altrimenti lascio valore
            var x = a ?? 1;//1 perchè a è null
            var y = b ?? 1;//123 perchè b non è null

            //?. null conditional

            //operatori
            //unari
            //binari
            //ternari
            //aritmetici
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

       
    }

}
