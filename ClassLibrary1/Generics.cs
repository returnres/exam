using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //constraeint 
    /*
     *
where T : struct	The type argument must be a non-nullable value type. Because all value types have an accessible parameterless constructor, the struct constraint implies the new() constraint and can't be combined with the new() constraint. You can't combine the struct constraint with the unmanaged constraint.
where T : class	The type argument must be a reference type. This constraint applies also to any class, interface, delegate, or array type. In a nullable context in C# 8.0 or later, T must be a non-nullable reference type.
where T : class?	The type argument must be a reference type, either nullable or non-nullable. This constraint applies also to any class, interface, delegate, or array type.
where T : notnull	The type argument must be a non-nullable type. The argument can be a non-nullable reference type in C# 8.0 or later, or a non-nullable value type.
where T : unmanaged	The type argument must be a non-nullable unmanaged type. The unmanaged constraint implies the struct constraint and can't be combined with either the struct or new() constraints.
where T : new()	The type argument must have a public parameterless constructor. When used together with other constraints, the new() constraint must be specified last. The new() constraint can't be combined with the struct and unmanaged constraints.
where T : <base class name>	The type argument must be or derive from the specified base class. In a nullable context in C# 8.0 and later, T must be a non-nullable reference type derived from the specified base class.
where T : <base class name>? The type argument must be or derive from the specified base class. In a nullable context in C# 8.0 and later, T may be either a nullable or non-nullable type derived from the specified base class.
where T : <interface name>	The type argument must be or implement the specified interface. Multiple interface constraints can be specified. The constraining interface can also be generic. In a nullable context in C# 8.0 and later, T must be a non-nullable type that implements the specified interface.
where T : <interface name>?	The type argument must be or implement the specified interface. Multiple interface constraints can be specified. The constraining interface can also be generic. In a nullable context in C# 8.0, T may be a nullable reference type, a non-nullable reference type, or a value type. T may not be a nullable value type.
where T : U	The type argument supplied for T must be or derive from the argument supplied for U. In a nullable context, if U is a non-nullable reference type, T must be non-nullable reference type. If U is a nullable reference type, T may be either nullable or non-nullable.
     * 
     * 
     */
    public interface ITest<T>
    {
        T GetDefault(T param);
    }

    public class Test<T> : ITest<T>
    {
        public T GetDefault(T param)
        {
            return default(T);
        }
    }

    public class Test :ITest<string>
    {
        public string GetDefault(string param)
        {
            return "";
        }
    }
   

    public class Gen2Figlia<T> : Gen2<T> where T : class
    {

    }


    public class Gen2<T> where T : class
    {

    }

    public class Gen1Figlia<T, T1> where T : ConstructorStaticClass
                                    where T1 : class
    {

    }

    public class Gen5<T, T1> where T : ConstructorStaticClass, Imiainterfaccia
        where T1 : Task
    {
    }

    public class Gen1
    {
        public T2 ContinueTaskOrDefault<T2>(T2 param,Action callBack)
        {
            if (param == null)
                return default(T2);
            var t = (Task)(object)param;
            t.ContinueWith(x => callBack);
            return param;
        }

        public T2[] ContinueTasksOrDefault<T2>(T2[] param,Action callBack)
        {
            if (param == null)
                return default(T2[]);
            var t = (Task[])(object)param;
            foreach (var item in t)
            {
                item.ContinueWith(x => callBack);
            }
            return param;
        }
    }



    public class Generica<T, U, Y, Z>
    {
        private T campo1;
        private U campo2;
        private Y campo3;
        private Z campo4;

        public Generica()
        {
            campo1 = default(T);
            campo2 = default(U);
            campo3 = default(Y);
            campo4 = default(Z);

            Console.WriteLine("1-{0}", campo1);
            Console.WriteLine("2-{0}", campo2);
            Console.WriteLine("3-{0}", campo3);
            Console.WriteLine("4-{0}", campo4);
        }
    }

    public class Lista1<T> : Lista<T>
    {
        /// <summary>
        /// è necessario solo se non metto il cstruttore di defult
        /// </summary>
        /// <param modello="len"></param>
        public Lista1(int len) : base(len)
        {
        }
    }

    public class Lista2<T, U> : Lista<T>
    {

        /// <summary>
        /// è necessario solo se non metto il cstruttore di defult
        /// </summary>
        /// <param modello="len"></param>
        public Lista2(int len) : base(len)
        {
        }
    }


    public class Lista<T>
    {
        private readonly T[] el;

        /// <summary>
        /// deafult crea array di 5 elementi
        /// </summary>
        public Lista()
        {
            el = new T[10];
        }

        /// <summary>
        /// crea array di n elelementi
        /// </summary>
        /// <param modello="len"></param>
        public Lista(int len)
        {
            el = new T[len];
        }

        public T this[int index]
        {
            get => el[index];

            set => el[index] = value;
        }
    }

    public class TestStatic
    {
        public static string Status;
    }

    public class TestStaticGen<T>
    {
        public static T Status;
    }

   
}
