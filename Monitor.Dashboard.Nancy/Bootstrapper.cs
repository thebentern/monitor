using Nancy;
using Nancy.Conventions;

namespace Monitor.Dashboard.Nancy
{
    /// <summary>
    /// Nancy bootstrapper for Dashboard
    /// </summary>
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// Configures the conventions.
        /// </summary>
        /// <param name="conventions">The conventions.</param>
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            //conventions.StaticContentsConventions.Add(
            //    StaticContentConventionBuilder.AddDirectory("Scripts", @"Scripts")
            //);
        }
    }
}