namespace SimpleMVC.App.MVC.Interfaces.Generic
{
    public interface IActionResult<T>:IInvokeable
    {
        IRenderable<T> Action { get; set; }
    }
}