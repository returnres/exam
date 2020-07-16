using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //Event
    //puo essere anche anstarct seales static
    //PUBLISHER
    public interface IVehicle
    {
        event Car.GiritMotoreEventHandler MotoreSpento;
    }

    public class GiritMotoreEventArgs : EventArgs
    {
        public int NumeroGiriRAggiunto { get; set; }
    }

   
    public class Car : IVehicle
    {
        //Generic
        //public delegate void EventHandler(Object obj, EventArgs e);
        //public event EventHandler MotoreSpento;

        public delegate void GiritMotoreEventHandler(Car car, GiritMotoreEventArgs e);
        public event GiritMotoreEventHandler MotoreSpento;

        private int _numerogiri;

        public Car()
        {
            _numerogiri = 50;
        }
        public void Decelerate()
        {
            _numerogiri -= 10;
            if (_numerogiri <= 30)
            {
                _numerogiri = 0;
                OnMotoreSpento();
            }
        }

        protected virtual void OnMotoreSpento()
        {
            //if (MotoreSpento != null)
            //{
            //    MotoreSpento(this, EventArgs.Empty);
            //}
            //oppure
            //MotoreSpento?.Invoke(this, EventArgs.Empty);
            //oppure
            MotoreSpento?.Invoke(this, new GiritMotoreEventArgs()
            {
                NumeroGiriRAggiunto = this._numerogiri
            });
        }
    }


    public class CarMonitor
    {
        public CarMonitor(Car car)
        {
            car.MotoreSpento += GestisciMororeSpento;
        }

        private void GestisciMororeSpento(object sender, GiritMotoreEventArgs e)
        {
            //gestisco evnto
            Car c = sender as Car;
            Console.WriteLine("numweo giri {0}", e.NumeroGiriRAggiunto);
            c.MotoreSpento -= GestisciMororeSpento;
        }
    }


    public class EletricCar : Car
    {
        protected override void OnMotoreSpento()
        {
            //codice personalizzato
            base.OnMotoreSpento();
        }
    }

  
}
