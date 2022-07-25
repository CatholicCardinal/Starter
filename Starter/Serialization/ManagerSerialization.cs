using Starter.Models;
using Starter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Serialization
{
    public class ManagerSerialization<T>
    {
        public ISerialization<T> serialization;

        private string methodSerialization="Excel";
        public string MethodSerialization { get => methodSerialization; set => methodSerialization = value; }
        public ManagerSerialization(string methodSerialization)
        {
            MethodSerialization = methodSerialization;
        }
        public void Export()
        {
            Activator.CreateInstance("Starter", "ExcelSerialization");

            Type genericType = typeof(ExcelSerialization<>);
            Type[] typeArgs = { Type.GetType("Starter.Models.Record") };
            Type repositoryType = genericType.MakeGenericType(typeArgs);

            object repository = Activator.CreateInstance(repositoryType);

            //string typeString = 
            //Type typeArgument = Type.GetType(string.Format("Starter.Serialization.{0}", methodSerialization + "Serialization"));
            //Type template = typeof(ISerialization<>);
            //Type genericType = template.MakeGenericType(typeArgument);

            //serialization = (ISerialization<T>)Activator.CreateInstance(genericType);
            //serialization = new ExcelSerialization<T>();
        }
    }
}
