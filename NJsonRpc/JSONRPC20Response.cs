using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonRpc.Exceptions;

namespace NJsonRpc
{
    public class JSONRPC20Response:JSONRPCBaseResponse
    {
        public JSONRPC20Response(string id, JSONRPCError error) : base(id, error)
        {
        }

        public JSONRPC20Response(string id, JToken result) : base(id, result)
        {
        }

        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string Jsonrpc { get; set; } = "2.0";
    }
}