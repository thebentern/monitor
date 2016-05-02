using Microsoft.Owin.Hosting;
using System;

namespace Monitor.Dashboard.Nancy
{
    /// <summary>
    /// Nancy Dashboard for Monitor
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the self-hosted web application
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            const string url = "http://+:8080";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
