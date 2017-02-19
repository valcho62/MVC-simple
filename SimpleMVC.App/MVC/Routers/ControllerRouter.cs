using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleHttpServer.Enums;
using SimpleHttpServer.Models;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Interfaces;

namespace SimpleMVC.App.MVC.Routers
{
    public class ControllerRouter:IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string actionName;
        private string controllerName;
        private object[] methodParams;

        private string[] controllerActionParams;
        private string[] controllerAction;

        public ControllerRouter()
        {
            this.getParams = new Dictionary<string, string>();
            this.postParams = new Dictionary<string, string>();
        }
        public HttpResponse Handle(HttpRequest request)
        {
            this.getParams = GetParams(request.Url);
            this.postParams = GetParams(request.Content);
            this.requestMethod = request.Method.ToString();
            ParseContrAndActionName(request.Url);

            MethodInfo method = GetMethod();

            IEnumerable<ParameterInfo> parameters =
                method.GetParameters();
            int index = 0;
            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.ParameterType.IsPrimitive)
                {
                    object value = this.getParams[parameter.Name];
                    this.methodParams[index] = Convert.ChangeType(
                        value,
                        parameter.ParameterType);
                    index++;
                }
                else if (parameter.ParameterType == typeof(HttpRequest) )
                {
                    this.methodParams[index] = request;
                    index++;
                }
                else if (parameter.ParameterType == typeof(HttpSession))
                {
                    this.methodParams[index] = request.Session.Id;
                    index++;
                }
                else
                {
                    Type bindingModelType = parameter.ParameterType;
                    object bindingModel = Activator
                        .CreateInstance(bindingModelType);
                    PropertyInfo[] properties =
                        bindingModelType.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(
                            bindingModel,
                            Convert.ChangeType(
                                postParams[property.Name],
                                property.PropertyType)
                                );
                    }

                    this.methodParams[index] = Convert.ChangeType(
                        bindingModel,
                        bindingModelType);
                    index++;
                }
            }

            IInvokeable actionResult =
                (IInvokeable) this.GetMethod()
                    .Invoke(this.GetController(), this.methodParams);
            string content = actionResult.Invoke();

            HttpResponse response =new HttpResponse()
            {
                StatusCode = ResponseStatusCode.Ok,
                ContentAsUTF8 = content
            };

            return response;
        }

        private Controller GetController()
        {
            string controllerType = string.Format(
                "{0}.{1}.{2}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ControllerFolder,
                this.controllerName
                );
            var controller = (Controller)
                Activator.CreateInstance(Type.GetType(controllerType));
            return controller;
        }
        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            return this.GetController()
                .GetType()
                .GetMethods()
                .Where(x => x.Name == this.actionName);
        }
        private MethodInfo GetMethod()
        {
            MethodInfo method = null;
            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {
                var attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(x => x is HttpMethodAttribute);

                if (!attributes.Any())
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

           return method;
        }

        private void ParseContrAndActionName(string requestUrl)
        {
            
            var bothNames = requestUrl.Split('?')[0];
            if (bothNames.Split('/').Length < 3)
            {
                return;
            }
            controllerName = bothNames.Split('/')[1];
            this.controllerName = controllerName.Substring(0, 1).ToUpper() + controllerName.Substring(1).ToLower()
                             + "Controller";
            actionName = bothNames.Split('/')[2];
            this.actionName = this.actionName.Substring(0, 1).ToUpper() + actionName.Substring(1).ToLower();

        }

        private IDictionary<string, string> GetParams(string requestUrl)
        {
            var result = new Dictionary<string,string>();
            if (requestUrl == null || requestUrl.Split('?').Length < 2)
            {
                return result;
            }
            var stringParams = requestUrl.Split('?')[1];
            var allParams = stringParams.Split('&');
            foreach (var param in allParams)
            {
                var name = param.Split('=')[0];
                var value = param.Split('=')[1];
                result.Add(name,value);
            }

            return result;
        }
        
    }
}