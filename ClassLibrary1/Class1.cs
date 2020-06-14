using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //astrazione
    //incapsulamento
    //ereditarietà
    //polimorfismo

    public class Babbo
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

        public Babbo()
        {
                datanascrita = DateTime.Now;
        }

        //OVERLOAD FIRMA = NOME +ARGOMENTI INGERSSO
        public int ripetiNumero(int a)
        {
            return a;
        }
        public int ripetiNumero(int a, int b)
        {
            return a;
        }

       
    }

    public class Figlio : Babbo
    {
        
    }


    public class Miaclasse : Miaclasse1
    {
        //membri
        public string pippo { get; set; }

        public string pluto
        {
            get { return pluto; }

            set { pluto = value; }
        }

        public string papaperino
        {
            get => papaperino;
            set => papaperino = value;
        }
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
