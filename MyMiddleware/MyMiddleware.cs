using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyMiddleware
{
    // use an alias for the OWIN AppFunc:
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class MyMiddleware
    {
        AppFunc _next;

        // Add a member to hold the greeting:
        //string _greeting;
        MyMiddlewareConfigOptions _configOptions;

        public MyMiddleware(AppFunc next, MyMiddlewareConfigOptions configOptions)
        {
            _next = next;
            _configOptions = configOptions;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            // If there is no next component, a 404 Not Found will be written as 
            // the response code here:
            await _next.Invoke(environment);

            IOwinContext context = new OwinContext(environment);

            // Insert the _greeting into the display text:
            await context.Response.WriteAsync(string.Format("<h1>{0}</h1>", _configOptions.GetGreeting()));

            // Update the response code to 200 OK:
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";
        }
    }
}
