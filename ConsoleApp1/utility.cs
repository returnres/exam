using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ConsoleApp1
{
   public static class Utility
    {
        public static T1 GetDefault<T, T1>(this Gen1<T,T1> obj, T1 param) where T : MiaclasseStatic where T1 : Task
        {
            return default(T1);
        }

        public static bool IsNumeric(this string str)
        {
            double res;
            return double.TryParse(str, out res);
        }
    }
}
