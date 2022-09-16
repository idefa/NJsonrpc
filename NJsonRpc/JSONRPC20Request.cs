using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NJsonRpc
{
    public class JSONRPC20Request:JSONRPCBaseRequest
    {
        public String JSONRPC_VERSION = "2.0";

        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string Jsonrpc { get; private set; }
    }
}