using System;

namespace ClassLibrary1
{

    //interfaccie IComparable

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

    public class Moto
    {
        public string Targa { get; set; }
    }
}