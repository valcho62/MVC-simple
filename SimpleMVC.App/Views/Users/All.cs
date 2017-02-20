

using System.Text;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;

namespace SimpleMVC.App.Views.Users
{
    public class All:IRenderable<AllUserViewModel>
    {
        public AllUserViewModel Models { get; set; }
        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h3>All users</h3>");
            sb.AppendLine("<ul>");
            foreach (var name in Models.Usernames)
            {
                sb.AppendLine($"<li>{name}</li>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
