using System;
using System.Collections.Generic;
using System.Reflection;

namespace NJsonRpc
{
    public class Dispatcher
    {
        private Dictionary<String, MethodInfo> method_map = new Dictionary<string, MethodInfo>();
    }
}