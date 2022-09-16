using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NJsonRpc
{
    public class JSONRPCBaseRequest
    {
        public String JSONRPC_VERSION;
        
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; set; }
        
        [JsonProperty("is_notification", Required = Required.Default)]
        public bool IsNotification { get; set; }

        [JsonProperty("params")]
        public JToken Params { get; set; }
    }
}