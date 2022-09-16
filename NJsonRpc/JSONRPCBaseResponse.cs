using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonRpc.Exceptions;

namespace NJsonRpc
{
    public class JSONRPCBaseResponse
    {
        public JSONRPCBaseResponse(String id,JSONRPCError error)
        {
            this.Id = id;
            this.Error = error;
        }

        public JSONRPCBaseResponse(String id,JToken result)
        {
            this.Id = id;
            this.Result = result;
        }
        
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("result", Required = Required.Default)]
        public JToken Result { get; set; }

        [JsonProperty("error", Required = Required.Default)]
        public JSONRPCError Error { get; set; }

    }
}