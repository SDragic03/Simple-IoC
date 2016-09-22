using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using IoCWebApp.Classes;
using IoCWebApp.DIContainer;

namespace IoCWebApp.Factories
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                var constructors = controllerType.GetConstructors();

                if (constructors.Length > 0)
                {
                    var constructor = constructors[0];

                    var constructorArguments = new List<object>();
                    var parameters = constructor.GetParameters();

                    foreach (var parameter in parameters)
                    {
                        object parameterInstance = null;

                        parameterInstance = Container.Resolve(parameter.ParameterType);

                        if (parameterInstance is Message)
                        {
                            // Test injecting user data
                            var message = requestContext.HttpContext.Request.QueryString["message"];

                            if (!string.IsNullOrEmpty(message))
                                ((Message)parameterInstance).SetMessage(message);
                        }
                        constructorArguments.Add(parameterInstance);
                    }

                    var result = Activator.CreateInstance(controllerType, constructorArguments.ToArray());
                    return (IController)result;
                }
            }
            return base.GetControllerInstance(requestContext, controllerType);
        }

        public static Container Container = new Container();
    }
}