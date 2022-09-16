namespace NJsonRpc.Exceptions
{
    public class JSONRPCParseError:JSONRPCError
    {
        public JSONRPCParseError()
        {
            this.CODE = -32700;
            this.MESSAGE = "Parse error";
        }
    }
}