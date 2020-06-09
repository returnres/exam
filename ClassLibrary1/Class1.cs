using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public enum Mesi
    {
        gennaio =0,
        febbraio=1
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

}
