using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    [DebuggerDisplay("name ={" + nameof(name) + "}")]
    public class Money : IComparable,IEquatable<Money>, IFormattable, IFormatProvider, ISerializable,IEnumerable<Money>,IDisposable
    {
        public string name  { get; set; }
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Money other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Money> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
        }
    }



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
