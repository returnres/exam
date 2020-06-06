using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public delegate int mydel();
    public delegate int mydel1(string str);
    public delegate string EmptyDel(string str);
    public delegate int EmptyDel1(ref int p);

    public class Del
    {
        public int MyCon(mydel1 callback, string tocon)
        {
            return callback(tocon);
        }

        public int MyCon1(Func<string,int> callback, string tocon)
        {
            return callback(tocon);
        }

        public void UseMulticast()
        {
            var m2 = new mydel1(Convert.ToInt32);

            var a = m2("3");
            var b = m2.Invoke("3");

            //dlegate è immutabile
            //internamente viene crato uin nuovo delegat
            EmptyDel multicast = ToLow;
            multicast += ToUp;
            Console.WriteLine(multicast("roberto"));
            multicast -= ToUp;
            int x = 0;
            EmptyDel1 multicast1 = Add1;
            multicast1 += Add2;
            multicast1(ref x);
            Console.WriteLine("RISULTATO = " +x);
        }


        public int Add1(ref int p)
        {
            return p++;
        }
        public int Add2(ref int p)
        {
            return p++;
        }
        public string ToUp(string txt)
        {
          return   txt.ToUpper();
        }

        public string ToLow(string txt)
        {
            return txt.ToLower();
        }
    }
}
