using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /*
     * 
     * An extension method must be defined in a top-level static class.
An extension method with the same name and signature as an instance method will not be called.
Extension methods cannot be used to override existing methods.
The concept of extension methods cannot be applied to fields, properties or events.
Overuse of extension methods is not a good style of programming.
     * 
     * 
     * 
     */
    public static  class ExtensionMethods
    {
        public static void faccio(this testa t, int bo)
        {
            Console.WriteLine(bo);
        }

    }


    public class testa
    {
        private List<int> _mialista;
        private string _a;
        public testa()
        {
         _mialista = new List<int>();

        }

        public testa(string a) :base()
        {
            _a = a;
        }
    }
}
