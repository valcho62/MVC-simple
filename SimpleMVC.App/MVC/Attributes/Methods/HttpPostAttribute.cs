using System;

namespace SimpleMVC.App.MVC.Attributes.Methods
{
    class HttpPostAttribute :HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToUpper() == "POST")
            {
                return true;
            }
            return false;
        }
    }
}
