using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    /// <summary>
    /// il raise scatena tutte le azioni che si sono registrate all'evento
    /// </summary>
    public class Pub
    {
        public Action OnChange { get; set; }

        public void Raise()
        {
            if (OnChange != null)
            {
                OnChange();
            }
        }
    }

    public class Pub1
    {
        public event Action OnChange = delegate { };

        public void Raise()
        {
            OnChange();
        }
    }

    public class Pub2
    {
        public event EventHandler<MyArgs> OnChange = delegate { };

        public void Raise()
        {
            OnChange(this, new MyArgs(1));
        }

        public void CreateAndRaise()
        {
            Pub2 p = new Pub2();
            p.OnChange += (sender, e) => Console.WriteLine($"rasise arg {e.Value}");
            p.Raise();
        }
    }

    public class Pub3
    {
        public delegate void Miodelegate();
        public event Miodelegate OnChange = delegate { };

        public void Raise()
        {
            OnChange();
        }
    }


    public class MyArgs : EventArgs
    {
        public MyArgs(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
