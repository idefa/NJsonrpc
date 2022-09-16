using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonRpc.Exceptions;

namespace NJsonRpc
{
    public static class JSONRPCResponseManager
    {
        public static Object Handle(String requestStr, Dictionary<String, Delegate> dispatcher)
        {
            JSONRPCBaseRequest request = null;
            try
            {
                request = JSONRPCRequest.FromJson(requestStr);
                
            }
            catch (Exception e)
            {
                return new JSONRPC20Response(null,new JSONRPCParseError());
            }
            return HandleRequest(request, dispatcher);
        }

        private static Object HandleRequest(JSONRPCBaseRequest request, Dictionary<String, Delegate> dispatcher)
        {
            return get_responses(request,dispatcher);
        }

        private static Object get_responses(JSONRPCBaseRequest request, Dictionary<String, Delegate> dispatcher)
        {
            Delegate func = null;
            try
            {
                func = dispatcher[request.Method];
            }
            catch (Exception e)
            {
                return make_response(request, new JSONRPCMethodNotFound());
            }
            MethodInfo method = func.Method;
            JToken[] orignialParameterList;
            if (request.Params == null) {
                orignialParameterList = new JToken[0];
            } else {
                switch (request.Params.Type) {
                    case JTokenType.Object:
                        JsonSerializer jsonSerializer = GetJsonSerializer();
                        Dictionary<string, JToken> parameterMap = request.Params.ToObject<Dictionary<string, JToken>>(jsonSerializer);
                        bool canParse = TryParseParameterList(method, parameterMap, out orignialParameterList);
                        if (!canParse) {
                            return false;
                        }
                        break;
                    case JTokenType.Array:
                        orignialParameterList = request.Params.ToArray();
                        break;
                    default:
                        orignialParameterList = new JToken[0];
                        break;
                }
            }
            ParameterInfo[] parameterInfoList = method.GetParameters();
            if (orignialParameterList.Length > parameterInfoList.Length) {
                //return GetMethodRpcException(method, "传递参数个数大于方法定义的个数");
            }
            object[] correctedParameterList = new object[parameterInfoList.Length];
            for (int i = 0; i < orignialParameterList.Length; i++) {
                ParameterInfo parameterInfo = parameterInfoList[i];
                JToken parameter = orignialParameterList[i];
                bool isMatch = ParameterMatches(parameterInfo, parameter, out object convertedParameter);
                if (!isMatch) {
                    if (request.Params.Type == JTokenType.Object) {
                        //throw GetMethodRpcException(method, "参数类型不匹配");
                    } else {
                        return false;
                    }
                }
                correctedParameterList[i] = convertedParameter;
            }
            if (orignialParameterList.Length < parameterInfoList.Length) {
                for (int i = orignialParameterList.Length; i < parameterInfoList.Length; i++) {
                    if (!parameterInfoList[i].IsOptional) {
                        //throw GetMethodRpcException(method, "缺少参数");
                    }
                    correctedParameterList[i] = Type.Missing;
                }
            }
            var result = func.DynamicInvoke(correctedParameterList);
            return result;
        }

        private static JSONRPCBaseResponse make_response(JSONRPCBaseRequest request,JSONRPCError error)
        {
            if (request.JSONRPC_VERSION=="1.0")
            {
                return new JSONRPC10Response(request.Id, error);
            }
            else
            {
                return new JSONRPC20Response(request.Id, error);
            }
        }
        
        private static JSONRPCBaseResponse make_response(JSONRPCBaseRequest request,JToken result)
        {
            if (request.JSONRPC_VERSION=="1.0")
            {
                return new JSONRPC10Response(request.Id, result);
            }
            else
            {
                return new JSONRPC20Response(request.Id, result);
            }
        }

        private static bool ParameterMatches(ParameterInfo parameterInfo, JToken value, out object convertedValue) {
            Type parameterType = parameterInfo.ParameterType;
            try {
                switch (value.Type) {
                    case JTokenType.Array: {
                            JsonSerializer serializer = GetJsonSerializer();
                            JArray jArray = (JArray)value;
                            convertedValue = jArray.ToObject(parameterType, serializer);
                            return true;
                        }
                    case JTokenType.Object: {
                            JsonSerializer serializer = GetJsonSerializer();
                            JObject jObject = (JObject)value;
                            convertedValue = jObject.ToObject(parameterType, serializer);
                            return true;
                        }
                    case JTokenType.String: {
                            if (parameterType == typeof(string)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.Integer: {
                            if (parameterType == typeof(int) || parameterType == typeof(long)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.Float: {
                            if (parameterType == typeof(float)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.Boolean: {
                            if (parameterType == typeof(bool)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.Date: {
                            if (parameterType == typeof(DateTime)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.Bytes: {
                            if (parameterType == typeof(byte)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.Guid: {
                            if (parameterType == typeof(Guid)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }
                    case JTokenType.TimeSpan: {
                            if (parameterType == typeof(TimeSpan)) {
                                convertedValue = value.ToObject(parameterType);
                                return true;
                            }
                            convertedValue = null;
                            return false;
                        }


                    default:
                        convertedValue = value.ToObject(parameterType);
                        return true;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                convertedValue = null;
                return false;
            }
        }

        private static bool TryParseParameterList(MethodInfo method, Dictionary<string, JToken> parametersMap, out JToken[] parameterList) {
            ParameterInfo[] parameterInfoList = method.GetParameters();
            parameterList = new JToken[parametersMap.Count()];
            foreach (ParameterInfo parameterInfo in parameterInfoList) {
                if (!parametersMap.ContainsKey(parameterInfo.Name) && !parameterInfo.IsOptional) {
                    parameterList = null;
                    return false;
                }
                parameterList[parameterInfo.Position] = parametersMap[parameterInfo.Name];
            }
            return true;
        }

        private static JsonSerializer GetJsonSerializer() {
            return JsonSerializer.CreateDefault();
        }

    }
}