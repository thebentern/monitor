using System;

using Microsoft.Owin.Hosting;

namespace Monitor.Dashboard.Nancy
{
    /// <summary>
    /// Nancy Dashboard for Monitor
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the self-hosted web application
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            const string Url = "http://+:8080";

            using (WebApp.Start<Startup>(Url))
            {
                Console.WriteLine("Running on {0}", Url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
