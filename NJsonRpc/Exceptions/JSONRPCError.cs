using System;
using Newtonsoft.Json;

namespace NJsonRpc.Exceptions
{
    // Error for JSON-RPC communication.
    //
    // When a rpc call encounters an error, the Response Object MUST contain the
    // error member with a value that is a Object with the following members:
    //
    // Parameters
    // ----------
    // code: int
    // A Number that indicates the error type that occurred.
    // This MUST be an integer.
    // The error codes from and including -32768 to -32000 are reserved for
    // pre-defined errors. Any code within this range, but not defined
    // explicitly below is reserved for future use. The error codes are nearly
    // the same as those suggested for XML-RPC at the following
    // url: http://xmlrpc-epi.sourceforge.net/specs/rfc.fault_codes.php
    //
    // message: str
    // A String providing a short description of the error.
    // The message SHOULD be limited to a concise single sentence.
    //
    // data: int or str or dict or list, optional
    // A Primitive or Structured value that contains additional
    // information about the error.
    // This may be omitted.
    // The value of this member is defined by the Server (e.g. detailed error
    // information, nested errors etc.).
    public class JSONRPCError
    {
        [JsonProperty("code", Required = Required.Always)]
        public int CODE { get; set; }

        [JsonProperty("message", Required = Required.Always)]
        public String MESSAGE { get; set; }
    }
}