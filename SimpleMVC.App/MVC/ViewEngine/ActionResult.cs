

using System;
using SimpleMVC.App.MVC.Interfaces;

namespace SimpleMVC.App.MVC.ViewEngine
{
    public class ActionResult :IActionResult
    {
        public IRenderable Action { get; set; }

        public ActionResult(string viewFullQualifiedName)
        {
            this.Action = (IRenderable) Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));
        }
        public string Invoke()
        {
            return this.Action.Render();
        }

        
    }
}
