using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
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


    public class Miaclasse : Miaclasse1
    {

    }

    public interface Imiainterfaccia
    {
    }

    public class Miaclasse1 : Imiainterfaccia
    {

    }

    public class TestClass1
    {
        /*
         * stack => vet
         * heap 1
         * heap 2
         * heap 3
         * 
        ARRAY MULTID:
        -array rettangoli
        1234
        1234
        
        -array jagged (irregolare)
        123456
        234
        2343434343
         */


        private int[] vet = new[] { 1, 2, 3 };

        public void MyMethod()
        {
            //ret
            int[,] matrix = new int[3, 4];
            matrix[0,0] = 1;

            int[,] matrix1 =
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8}
            };

            //jagged
            int[][] jagged = new int[3][];
            jagged[0] = new int[2] {1, 2};
            jagged[1] = new int[4] {1, 2,3,4};
        }

    }
}
