namespace NJsonRpc.Exceptions
{
    public class JSONRPCInvalidRequest:JSONRPCError
    {
        public JSONRPCInvalidRequest()
        {
            this.CODE = -32600;
            this.MESSAGE = "Invalid Request";
        }
    }
}