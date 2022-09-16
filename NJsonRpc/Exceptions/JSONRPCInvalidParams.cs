namespace NJsonRpc.Exceptions
{
    public class JSONRPCInvalidParams:JSONRPCError
    {
        public JSONRPCInvalidParams()
        {
            this.CODE = -32602;
            this.MESSAGE = "Invalid params";
        }
    }
}