using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
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
