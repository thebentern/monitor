using Nancy;

namespace Monitor.Dashboard.Nancy.Modules
{
    public class MonitorModule : NancyModule
    {
        public MonitorModule()
        {
            Get["/monitor"] = _ =>
            {
                return View["monitor"];
            };
        }
    }
}