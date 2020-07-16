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
    public class Money : IComparable,IEquatable<Money>, IFormattable, IFormatProvider, ISerializable,IEnumerable<Money>
    {
        public string name  { get; set; }
        public int CompareTo(object obj)
        {
            if (obj is Money)
            {
                Money other = obj as Money;
                return String.Compare(this.name, other.name, StringComparison.Ordinal);
            }
            return -1;
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

        public bool Equals(Money other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"name = {name}";
        }
    }

    public class Moto
    {
        public string Targa { get; set; }
    }
    public class ComparableMoto : Moto, IComparable
    {
        public int CompareTo(object obj)
        {
            if (obj is ComparableMoto)
            {
                ComparableMoto other = obj as ComparableMoto;
                return this.Targa.CompareTo(other.Targa);
            }
            return -1;
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
        public static explicit operator PippoTest(int i)
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
            return (PippoTest)p.Valore++;
        }
    }
}
