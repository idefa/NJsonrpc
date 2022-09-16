namespace NJsonRpc.Exceptions
{
    public class JSONRPCServerError:JSONRPCError
    {
        public JSONRPCServerError()
        {
            this.CODE = -32000;
            this.MESSAGE = "Server error";
        }
    }
}