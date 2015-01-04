using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace MyMiddleware
{
    public static class AppBuilderExtensions
    {
        public static void UseMyMiddleware(this IAppBuilder app, MyMiddlewareConfigOptions configOptions)
        {
            app.Use<MyMiddleware>(configOptions);
        }
    }
}
