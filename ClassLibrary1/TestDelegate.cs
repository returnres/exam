using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public delegate int mydel();
    public delegate int mydel1(string str);
    public delegate string EmptyDel(string str);
    public delegate int EmptyDel1(ref int p);

    //covariante out ritorno
    //controvariante in inrgresso
    //public delegate TDest ConvDel<TDest, TOrg>(TOrg val);
    public delegate TDest ConvDel<out TDest, in TOrg>(TOrg val);

    //esempio cov
    public delegate T Cov<out T>();
    public delegate void Con<in T>(T par);

    public delegate TDest ConvDel1<TDest, TOrg>(TOrg val) where TDest : class;
    
    //Func Action Predicate
    //delegate TResult Func<out TResult>();
    //delegate TResult Func<in T, TResult>();
    //delegate  Action<in T>();
  
    public class Del
    {

        public string Cov1()
        {
            return "test";
        }

        public object Cov2()
        {
            return "test";
        }

        public void Con1(object par)
        {
        }

        public void Con2(string par)
        {
        }

        public TDest  MyConGen<TDest, TOrg>(ConvDel<TDest, TOrg> callback, TOrg par)
        {
            return callback(par);
        }

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
            //dlegate è immutabile
            //internamente viene crato uin nuovo delegat
            //il delegate è un tipo e
            //quindi adesso assegno al
            //multicast di tipo delegate
            //un metodo che rispetta la firma del delegate
            //e poi uso la variabile di tipo delegate
            //come se stessi usando il metodo
            //posso assegnare ad un delegate in 4 modi :
            //1 metodo anonimo delegate{espressione}
            //2 lambda 
            //3 nome della funzione
            //4 espressione

            EmptyDel multicast = ToLow;
            multicast += ToUp;

            //metodo anonimo
            multicast = delegate { return ""; };
            multicast = Convert.ToString;
            
            //lambda expression C#6
            //(parametro) => espressione
            string mystring = "closure";
            multicast = s => mystring;//CLOSURE uso variabile dichiarata fuori
            //ATTENZIONE LA VARIABILE MYSTRING VIENE VALUTATA QUANDO VIENE USATO
            //IL DELEGATE!!!
            mystring = "closure!";//multicast stampa closure!
            Console.WriteLine(multicast("roberto"));
            int x = 0;
            EmptyDel1 multicast1 = Add1;
            multicast1 += Add2;
            multicast1(ref x);

            Console.WriteLine("RISULTATO = " +x);
            multicast -= ToUp;

            var m2 = new mydel1(Convert.ToInt32);
            var a = m2("3");
            var b = m2.Invoke("3");
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
