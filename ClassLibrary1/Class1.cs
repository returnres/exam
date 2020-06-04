using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    public class Generica<T, U, Y, Z>
    {
        private T campo1;
        private U campo2;
        private Y campo3;
        private Z campo4;

        public Generica()
        {
            campo1 = default(T);
            campo2 = default(U);
            campo3 = default(Y);
            campo4 = default(Z);

            Console.WriteLine("1-{0}", campo1);
            Console.WriteLine("2-{0}", campo2);
            Console.WriteLine("3-{0}", campo3);
            Console.WriteLine("4-{0}", campo4);
        }
    }

    public class Lista1<T> : Lista<T>
    {
        /// <summary>
        /// è necessario solo se non metto il cstruttore di defult
        /// </summary>
        /// <param name="len"></param>
        public Lista1(int len) : base(len)
        {
        }
    }

    public class Lista2<T, U> : Lista<T>
    {
       
        /// <summary>
        /// è necessario solo se non metto il cstruttore di defult
        /// </summary>
        /// <param name="len"></param>
        public Lista2(int len) : base(len)
        {
        }
    }


    public class Lista<T>
    {
        private readonly T[] el;

        /// <summary>
        /// deafult crea array di 5 elementi
        /// </summary>
        public Lista()
        {
            el = new T[10];
        }

        /// <summary>
        /// crea array di n elelementi
        /// </summary>
        /// <param name="len"></param>
        public Lista(int len)
        {
            el = new T[len];
        }

        public T this[int index]
        {
            get => el[index];

            set => el[index] = value;
        }
    }

    public class TestStatic
    {
        public static string Status;
    }

    public class TestStaticGen<T>
    {
        public static T Status;
    }

}
