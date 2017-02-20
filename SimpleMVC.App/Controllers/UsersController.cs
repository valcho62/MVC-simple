using System.Collections.Generic;
using System.Linq;
using SimpleHttpServer.Models;
using SimpleMVC.App.BindingModels;
using SimpleMVC.App.Data;
using SimpleMVC.App.Models;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;

namespace SimpleMVC.App.Controllers
{
    public class UsersController :Controller
    {
        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            var contex = new NotesAppContext();
            var user = contex.Users.Find(model.UserId);
            var note = new Note()
            {
                Content = model.Content,
                Title = model.Title,
            };
            user.Notes.Add(note);
            contex.SaveChanges();

            return Profile(model.UserId);
   ;     }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            var contex = new NotesAppContext();
            var user = contex.Users.Find(id);
            var model = new UserProfileViewModel()
            {
                Username = user.Username,
                UserId = user.Id,
                Notes = user.Notes
                .Select(x => new NoteViewModel()
                {
                    Title = x.Title,
                    Content = x.Content
                }).ToList()

        };
           
            return this.View(model);
        }
        [HttpGet]
        public IActionResult<AllUserViewModel> All()
        {
            IList<string> usernames = null;
            var contex = new NotesAppContext();
            usernames = contex.Users.Select(x => x.Username).ToList();

            var result = new AllUserViewModel()
            {
                Usernames = usernames
            };

            return this.View(result);
        }
        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password,
                Notes = new List<Note>()
            };
            var contex = new NotesAppContext();
            contex.Users.Add(user);
            contex.SaveChanges();

            return this.View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
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