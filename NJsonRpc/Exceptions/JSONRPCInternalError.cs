namespace NJsonRpc.Exceptions
{
    public class JSONRPCInternalError:JSONRPCError
    {
        public JSONRPCInternalError()
        {
            this.CODE = -32603;
            this.MESSAGE = "Internal error";
        }
    }
}