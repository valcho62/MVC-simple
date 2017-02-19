namespace SimpleMVC.App.MVC.Interfaces.Generic
{
    public interface IRenderable<T> :IRenderable
    {
        T Models { get; set; }
    }
}