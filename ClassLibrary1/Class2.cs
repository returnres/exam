using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public struct PippoTest
    {
        public int Valore;

        public PippoTest(int valore) { Valore = valore; }

        //avanti
        public static implicit operator int(PippoTest pippoTest)
        {
            return pippoTest.Valore;
        }

        //indietro
        public static implicit operator PippoTest(int i)
        {
            return new PippoTest(i);
        }

        //binario
        public static int operator +(PippoTest p, int i)
        {
            return p.Valore + i;
        }

        //unario: il valore di ritorno deve essere lo stesso
        public static PippoTest operator ++(PippoTest p)
        {
            return p.Valore++;
        }
    }
}
