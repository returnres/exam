using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
    }


   

    public class Lista<T>
    {
        private readonly T[] el;

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


}
