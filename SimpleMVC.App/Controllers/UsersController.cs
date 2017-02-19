using SimpleHttpServer.Models;
using SimpleMVC.App.Models;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;

namespace SimpleMVC.App.Controllers
{
    public class UsersController :Controller
    {
        public IActionResult<GreetViewModel> Great(HttpSession session)
        {
            var greatViewModel = new GreetViewModel()
            {
                Session = session.Id
            };
            return View(greatViewModel);
        }
    }
}