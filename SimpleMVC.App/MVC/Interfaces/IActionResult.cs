namespace SimpleMVC.App.MVC.Interfaces
{
    public interface IActionResult:IInvokeable
    {
         IRenderable Action { get; set; }
    }
}