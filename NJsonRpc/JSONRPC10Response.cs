using System;
using Newtonsoft.Json.Linq;
using NJsonRpc.Exceptions;

namespace NJsonRpc
{
    public class JSONRPC10Response:JSONRPCBaseResponse
    {
        public const String JSONRPC_VERSION = "1.0";

        public JSONRPC10Response(string id, JSONRPCError error) : base(id, error)
        {
        }

        public JSONRPC10Response(string id, JToken result) : base(id, result)
        {
        }
    }
}