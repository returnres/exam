using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin1
{
    public class ClassePlugin1 
    {
        private string _surname;
        private int eta;

        public ClassePlugin1()
        {
                
        }
        public ClassePlugin1(string surname)
        {
            _surname = surname;
        }
        public string MioMetodo(int a)
        {
            return "";
        }
    }
    //public class Father
    //{
    //    protected string name { get; set; }
    //}
    //public class ClassePlugin1 : Father, IClass1
    //{
    //    private string _surname;
    //    private int eta;

    //    public ClassePlugin1(string surname)
    //    {
    //        _surname = surname;
    //    }
    //    public string MioMetodo(int a)
    //    {
    //        return "";
    //    }
    //}

    //public interface IClass1
    //{
    //    string MioMetodo(int a);

    //}
}
