using System;
using System.Runtime.CompilerServices;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.MVC.ViewEngine;
using SimpleMVC.App.MVC.ViewEngine.Generic;

namespace SimpleMVC.App.MVC.Controllers
{
    public class Controller
    {
        protected IActionResult View([CallerMemberName]string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Current.ControllerSuffix, String.Empty);
            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ViewsFolder,
                controllerName,
                caller);
            return new ActionResult(fullQualifiedName);
        }
        protected IActionResult View(string controller,string caller)
        {
            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ViewsFolder,
                controller,
                caller);
            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<T> View<T>(T model,[CallerMemberName]string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Current.ControllerSuffix, String.Empty);
            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ViewsFolder,
                controllerName,
                caller);
            return new ActionResult<T>(fullQualifiedName,model);
        }
        protected IActionResult<T> View<T>(T model,string controller, string caller)
        {
            string fullQualifiedName = string.Format(
                "{0}.{1}.{2}.{3}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ViewsFolder,
                controller,
                caller);
            return new ActionResult<T>(fullQualifiedName,model);
        }
    }
}