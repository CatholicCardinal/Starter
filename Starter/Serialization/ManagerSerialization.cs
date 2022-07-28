using Starter.Serialization.Implementation;

namespace Starter.Serialization
{
    public class ManagerSerialization<T>
    {
        public ISerialization<T> serialization;

        private string methodSerialization;
        public string MethodSerialization { get => methodSerialization; set => methodSerialization = value; }
        public ManagerSerialization(string methodSerialization)
        {
            MethodSerialization = methodSerialization;
        }
        public void Export(string filePath, object data)
        {
            switch (methodSerialization)
            {
                case "Excel":
                    serialization = new ExcelSerialization<T>();
                    break;
                case "Xml":
                    serialization = new XmlSerialization<T>();
                    break;
                default:
                    break;
            }

            serialization.Serialization(filePath, data);
            serialization.Dispose();

            //Type genericType = typeof(ExcelSerialization<>);
            //Type[] typeArgs = { Type.GetType("Starter.Models.Record") };
            //Type repositoryType = genericType.MakeGenericType(typeArgs);
            //object repository = Activator.CreateInstance(repositoryType);

            //string typeString = 
            //Type typeArgument = Type.GetType(string.Format("Starter.Serialization.{0}", methodSerialization + "Serialization"));
            //Type template = typeof(ISerialization<>);
            //Type genericType = template.MakeGenericType(typeArgument);

            //serialization = (ISerialization<T>)Activator.CreateInstance(genericType);
            //serialization = new ExcelSerialization<T>();
        }
    }
}
