using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }
    }

    [Serializable]
    public class PersonComplex : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private bool isDirty = false;
        public PersonComplex() { }
        protected PersonComplex(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32("value");
            Name = info.GetString("value"); 
            isDirty = info.GetBoolean("value"); 
        }

        [System.Security.Permissions.SecurityPermission(SecurityAction.Demand,
            SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("value", Id);
            info.AddValue("value", Name);
            info.AddValue("value", isDirty);
        }
    }



}
