using System;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;

namespace SimpleMVC.App.MVC.ViewEngine.Generic
{
    public class ActionResult<T>:IActionResult<T>
    {
        public IRenderable<T> Action { get; set; }

        public ActionResult(string viewFullQualifiedName, T model)
        {
            this.Action = (IRenderable<T>)Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));
            this.Action.Models = model;
        }
        public string Invoke()
        {
            return this.Action.Render();
        }



    }
}