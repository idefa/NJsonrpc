namespace NJsonRpc.Exceptions
{
    public class JSONRPCMethodNotFound:JSONRPCError
    {
        public JSONRPCMethodNotFound()
        {
            this.CODE = -32601;
            this.MESSAGE = "Method not found";
        }
    }
}