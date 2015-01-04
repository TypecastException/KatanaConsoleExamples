using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// Add the Owin Usings:
using Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;

// Add references to separate assemblies:
using SillyAuthentication;
using SillyLogging;
using MyMiddleware;

namespace KatanaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp.Start<Startup>("http://localhost:8080");
            Console.WriteLine("Server Started; Press enter to Quit");
            Console.ReadLine();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseSillyLogging();
            app.UseSillyAuthentication();

            // Set up the configuration options:
            var options = new MyMiddlewareConfigOptions("Greetings!", "John");
            options.IncludeDate = true;

            app.UseMyMiddleware(options);
        }
    }
}
