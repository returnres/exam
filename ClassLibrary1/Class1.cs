using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //astrazione
    //incapsulamento prop
    //ereditarietà
    //polimorfismo


    //INTERFACCIA DEFINISCE UN CONTRATTO
    public interface IInterface 
    {
        void Metodo();
        int Prop { get; set; }
    }

    public interface IInterface1
    {
        void Metodo();
       
    }
    //CLASSE ASTRATTA RAGGRUPPA FUNZIONALITA
    //deve avere almeno un metodo abstract e
    //la classe che la eredita deve avere override del metodo astratto
    //e implementarlo altrimenti diventa astratta
    public abstract class AMyclass : IInterface 
    {
        public void Validopertutti()
        {
            Console.WriteLine("Validopertutti");
        }
        public abstract void mymethod();

         void IInterface.Metodo()
         {
             this.mymethod();
         }

        public int Prop { get; set; }
        
    }

    public class Babbo
    {
        public Babbo(string pippo)
        {

        }
        //public Babbo()
        //{

        //}
    }

    public class Figlio :Babbo
    {
        //se il babbo ha un costruttore con parametro 
        //allora devo mettere esplicitamente al babbo costruttore  
        //senza parametri
        //errore compilazione cerca il costruttore
        //base senza parametri e non  lo trova !!!!
        //public Figlio()
        //{
        //    Console.WriteLine("ctor Figlio");
        //}

        //devo mettere questo oppure creare un costruttore con 0 parametri
        //al babbo
        public Figlio(string pippo) : base(pippo)
        {
        }
    }

    public class Veicolo
    {
        protected string Modello;
        protected int vel;
        public Veicolo(string modello)
        {
            Console.WriteLine("ctor babbo");

            Modello = modello;
        }

        public void Parti()
        {
            vel = 1;
        }

        protected void Accelera()
        {
            
        }

       public virtual void Decelerea()
        {

        }
    }

    public sealed class Macchina : Veicolo
    {
        private string _marca;

        public Macchina(string modello, string marca) : base(modello)
        {
            Console.WriteLine("ctor figlio");
            _marca = marca;
        }

        //HIDING
        protected void Accelera()
        {
            base.Parti();
        }

        public override void Decelerea()
        {
            //faccio codice diverso dal veicolo
        }
    }

    //C#7 expression body per metodi
    public class Miaclasse : MiaclasseStatic
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
        protected string _nome { get; set; }
        protected internal int anni { get; set; }
        public string pippo { get; set; }

        public string pluto
        {
            get { return pluto; }

            set { pluto = value; }
        }

        public Miaclasse()
        {
            datanascrita = DateTime.Now;
        }

        //expression body
        public Miaclasse(string nome) => _nome = nome;

        //expression body C#7
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
    public interface Imiainterfaccia
    {
    }

    public class MiaclasseStatic : Imiainterfaccia
    {
        public static int contatore { get; internal set; }

        static MiaclasseStatic()
        {
            contatore++;
        }
    }

    //indicizzatori

    //operator
    //public class OperatorCalss
    //{
    //    public OperatorCalss(OperatorCalss oc1, OperatorCalss oc2)
    //    {

    //    }
    //    public int pippo;
    //    public static OperatorCalss operator +(OperatorCalss oc1, OperatorCalss oc2)
    //    {

    //        return new OperatorCalss(oc1.pippo + oc2.pippo);
    //    }

    //    public static explicit operator OperatorCalss(int i)
    //    {
    //        return new OperatorCalss(i, 0);
    //    }
    //}

    public class SmartPhone
    {
        public class Battery
        {
            
        }
    }

    //file MyClass.cs
    partial class MyClass
    {
        
    }

    //secondMyClass.cs
    partial class MyClass
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
    public class MyClass1 : AMyclass
    {
        public override void mymethod()
        {
            throw new System.NotImplementedException();
        }
    }
}
