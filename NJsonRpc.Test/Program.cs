using System;
using System.Collections.Generic;

namespace NJsonRpc.Test
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            var dispatcher=new Dictionary<String, Delegate>();
            dispatcher.Add("add",new Func<int, int, int>(Sum));
            var result=JSONRPCResponseManager.Handle("{'id':'111','method':'add','params':[1,2]}", dispatcher);
            Console.WriteLine(result);
            Console.ReadLine();
        }
        
        static int Sum(int x, int y)
        {
            return x + y;
        }
    }
}