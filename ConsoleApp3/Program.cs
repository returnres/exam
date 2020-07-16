using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            double value = 12345.6789;

            //use any european culture
            Console.OutputEncoding = Encoding.UTF8;
            var cultureInfo = CultureInfo.GetCultureInfo("it-IT");
           
            Console.WriteLine(String.Format(cultureInfo, "{0:C} Euro", value));//12.345,68 € Euro
            Console.WriteLine($"€{value:N2} Euro");//€12.345,68 Euro
            Console.WriteLine($"{value:C}");//€12.345,68 Euro
            Console.WriteLine(value.ToString("C", CultureInfo.CurrentCulture));//12.345,68 €

            var res =  String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            var res1 = $"{DateTime.Now:dd/MM/yyyy}";
            Console.WriteLine(res);
            Console.WriteLine();
            Console.WriteLine(res1);
            var m = 2500.3;

            var uu = m.ToString("0.00");//stamperà 2500,30

            var uu1=  m.ToString("#.##"); //stamperà 2500,3
            var uu2 = m.ToString("#,#.00"); //stamperà 2.500,30
            Console.WriteLine(uu);
            Console.WriteLine();
            Console.WriteLine(uu1);
            Console.WriteLine();
            Console.WriteLine(uu2);
            #region CAS

            FileIOPermission f = new FileIOPermission(PermissionState.None);
            f.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                f.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }

            #endregion
        }

        [FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        public static  void DeclarativeCAS()
        {
            // Method body
        }
    }
}
