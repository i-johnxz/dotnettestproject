//using System;
//using System.Reflection;
//using Newtonsoft.Json.Linq;
//using LLY.Interface.Broker;
//using LLY.Interface.Broker.Request;

//namespace testtypeget
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
            
//            //Type type = Type.GetType("testtypeget.BaseRequest");
//            //JObject jObject = JObject.Parse(@"{""Id"":300001,""Name"":""johnxiong""}");
//            //var obj = jObject.ToObject(type);

            
//            var type = typeof(GetBalanceRequest).FullName;
//            var type1 = Type.GetType("LLY.Interface.Broker.Request.GetBalanceRequest, LLY.Interface.Broker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
//            Console.WriteLine(type);
//            Console.WriteLine("End");
//            Console.Read();
//        }

//        public static String GetAssemblyNameContainingType(String typeName)
//        {
//            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
//            {
//                Type t = currentassembly.GetType(typeName, false, true);
//                if (t != null) { return currentassembly.FullName; }
//            }

//            return "not found";
//        }
//    }

//    public class BaseRequest
//    {
//        public long Id { get; set; }

//        public string Name { get; set; }
//    }
//}
