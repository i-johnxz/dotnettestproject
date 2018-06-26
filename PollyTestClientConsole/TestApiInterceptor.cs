using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WebApiClient;
using WebApiClient.Contexts;

namespace PollyTestClientConsole
{
    public class TestApiInterceptor: WebApiClient.Defaults.ApiInterceptor
    {
        public TestApiInterceptor(HttpApiConfig httpApiConfig) 
            : base(httpApiConfig)
        {
        }

        public override object Intercept(object target, MethodInfo method, object[] parameters)
        {
            Console.WriteLine($"正在请求方法{method.Name}");
            return base.Intercept(target, method, parameters);
        }

        protected override ApiActionDescriptor GetApiActionDescriptor(MethodInfo method, object[] parameters)
        {
            return base.GetApiActionDescriptor(method, parameters);
        }
    }
}
