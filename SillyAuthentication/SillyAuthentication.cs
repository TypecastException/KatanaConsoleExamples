using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

namespace SillyAuthentication
{
    // use an alias for the OWIN AppFunc:
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class SillyAuthentication
    {
        AppFunc _next;
        public SillyAuthentication(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            // In the real world we would do REAL auth processing here...

            var isAuthorized = context.Request.QueryString.Value == "john";
            if (!isAuthorized)
            {
                context.Response.StatusCode = 401;
                context.Response.ReasonPhrase = "Not Authorized";

                // Send back a really silly error page:
                await context.Response.WriteAsync(string.Format("<h1>Error {0}-{1}",
                    context.Response.StatusCode,
                    context.Response.ReasonPhrase));
            }
            else
            {
                // _next is only invoked is authentication succeeds:
                context.Response.StatusCode = 200;
                context.Response.ReasonPhrase = "OK";
                await _next.Invoke(environment);
            }
        }
    }
}