using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NJsonRpc
{
    public static class JSONRPCRequest
    {
        public static JSONRPCBaseRequest FromJson(String json_str)
        {
            JObject jObject = JsonConvert.DeserializeObject(json_str) as JObject;
            return FromData(jObject);
        }
        
        public static JSONRPCBaseRequest FromData(JObject jObject)
        {
            string jsonrpc = jObject["jsonrpc"]?.ToString();
            JSONRPCBaseRequest request = null;
            if (jsonrpc == null)
            {
                request = jObject.ToObject<JSONRPC10Request>();
            }
            else
            {
                request = jObject.ToObject<JSONRPC20Request>();
            }
            return request;
        }
    }
}