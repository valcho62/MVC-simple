using SimpleMVC.App.MVC.Interfaces;

namespace SimpleMVC.App.Views.Users
{
    public class Great:IRenderable
    {
        
            public string Render()
            {
                return string.Format($"<h3>Hello user with sessionId : {0}</h3>");
            }
       
    }
}