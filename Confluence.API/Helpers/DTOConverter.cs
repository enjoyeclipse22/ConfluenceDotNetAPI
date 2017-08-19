using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace StopWatch.Confluence.DTO
{
    public static class DTOConverter
    {
        public static List<T> Convert<T>(this XmlRpcStruct[] rpcStructs)
        {
            return rpcStructs.Select(Convert<T>).ToList();
        }

        public static XmlRpcStruct Convert<T>(this T obj)

        {
            var rpcStruct = new XmlRpcStruct();

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                try
                {
                    var value = propertyInfo.GetValue(obj);
                    if (!rpcStruct.ContainsKey(propertyInfo.Name) && value != null)
                    {
                        rpcStruct[propertyInfo.Name] = value;
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            return rpcStruct;
        }

        public static T Convert<T>(this XmlRpcStruct rpcStruct)
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var key in rpcStruct.Keys)
            {
                PropertyInfo prop = obj.GetType().GetProperty(key.ToString());

                try
                {
                    //Set property value
                    object value = rpcStruct[key];
                    if (prop.CanWrite)
                    {
                        prop.SetValue(obj, value);
                    }
                }
                catch
                {

                }
            }
            return obj;
        }
    }
}
