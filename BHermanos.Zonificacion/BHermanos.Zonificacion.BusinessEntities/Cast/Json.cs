using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities.Cast
{

    public static class JsonSerializer
    {
        public static T Parse<T>(string jsonString)
        {
            using (var ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string stringfy(object jsonObject)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }

}
