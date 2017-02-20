using System;
using SimpleHttpServer;
using SimpleMVC.App.Data;
using SimpleMVC.App.MVC;


namespace SimpleMVC.App
{
    class AppStart
    {
        static void Main()
        {
            //NotesAppContext contex =new NotesAppContext();
            //contex.Database.Initialize(true);
            HttpServer server = new HttpServer(8081,RouteTable.Routes);
            MvcEngine.Run(server);
        }
    }
}
