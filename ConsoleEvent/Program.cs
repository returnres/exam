using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher p = new Publisher();
            Subscriber s = new Subscriber(p);//mi sottoscrivo ad evento
            int count = 0;

            for (int i = 0; i < 30; i++)
            {
                if (i - count == 0)
                {
                    p._numerogiri = i;
                    p.OnMotoreSpento();
                    Thread.Sleep(2000);
                    count += 10;
                }
            }


            Publisher1 p1 = new Publisher1();
            Subscriber1 s1 = new Subscriber1(p1);//mi sottoscrivo ad evento
            int count1 = 0;

            for (int i = 0; i < 30; i++)
            {
                if (i - count1 == 0)
                {
                    p1._numerogiri = i;
                    p1.OnMotoreSpento();
                    Thread.Sleep(2000);
                    count1 += 10;
                }
            }
        }
    }

    public class Subscriber
    {
        public Subscriber(Publisher pub)
        {
            Console.WriteLine("mi sottoscrivo ad evento");
            pub.MotoreSpento += Handle;
        }

        private void Handle(object sender, GiritMotoreEventArgs e)
        {
            Publisher c = sender as Publisher;
            Console.WriteLine("numero giri {0}", e.NumeroGiriRAggiunto);
        }
    }

    public class Publisher
    {
        //creo delegate
        //creo evento con il tipo delegate
        //sottoscivo metodi ad evento 
        //invoco metodo

        public delegate void GiritMotoreEventHandler(Publisher pub, GiritMotoreEventArgs e);
        public event GiritMotoreEventHandler MotoreSpento;

        public int _numerogiri;
        public void OnMotoreSpento()
        {
            Console.WriteLine("OnMotoreSpento");

            MotoreSpento?.Invoke(this, new GiritMotoreEventArgs()
            {
                NumeroGiriRAggiunto = this._numerogiri
            });
        }
    }

    public class GiritMotoreEventArgs : EventArgs
    {
        public int NumeroGiriRAggiunto { get; set; }
    }


    public class Subscriber1
    {
        public Subscriber1(Publisher1 pub)
        {
            Console.WriteLine("mi sottoscrivo ad evento");
            pub.MotoreSpento += Handle;
        }

        private void Handle(object sender, EventArgs e)
        {
            Publisher1 c = sender as Publisher1;
            Console.WriteLine("OnMotoreSpento ");

        }
    }

    public class Publisher1
    {
        public event EventHandler MotoreSpento;


        public int _numerogiri;
        public void OnMotoreSpento()
        {
            Console.WriteLine("OnMotoreSpento");

            MotoreSpento?.Invoke(this, new GiritMotoreEventArgs()
            {
                NumeroGiriRAggiunto = this._numerogiri
            });
        }
    }
}
