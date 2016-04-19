using Nancy;

namespace Monitor.Dashboard.Nancy.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ =>
            {
                return View["index"];
            };
        }
    }
}