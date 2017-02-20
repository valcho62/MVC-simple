using SimpleMVC.App.Models;

namespace SimpleMVC.App.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NotesAppContext : DbContext
    {

        public NotesAppContext()
            : base("name=NotesAppContext")
        {
            //MigrateDatabaseToLatestVersion
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}