using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NJsonRpc
{
    // JSON-RPC 1.0 Request.
    //     A remote method is invoked by sending a request to a remote service.
    //     The request is a single object serialized using json.
    //
    //     :param str method: The name of the method to be invoked.
    //     :param list params: An Array of objects to pass as arguments to the method.
    //     :param _id: This can be of any type. It is used to match the response with the request that it is replying to.
    //     :param bool is_notification: whether request notification or not.

    public class JSONRPC10Request:JSONRPCBaseRequest
    {
        public String JSONRPC_VERSION = "1.0";
    }
}