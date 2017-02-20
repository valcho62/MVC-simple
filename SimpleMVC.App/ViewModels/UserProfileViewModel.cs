

using System.Collections.Generic;

namespace SimpleMVC.App.ViewModels
{
   public  class UserProfileViewModel
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public ICollection<NoteViewModel> Notes { get; set; }
    }
}
