using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace SillyLogging
{
    public static class AppBuilderExtensions
    {
        public static void UseSillyLogging(this IAppBuilder app)
        {
            app.Use<SillyLogging>();
        }
    }
}
