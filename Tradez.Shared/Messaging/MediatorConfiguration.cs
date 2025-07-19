using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Messaging.Abstractions;

namespace Tradez.Shared.Messaging
{
    public static class MediatorConfiguration
    {
        public static void AddMediatorServices(this IServiceCollection services, Assembly assembly)
        {
            services.AddSingleton<IMediator, Mediator>();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface) continue;

                var interfaces = type.GetInterfaces();

                foreach (var iface in interfaces)
                {
                    if (iface.IsGenericType)
                    {
                        var def = iface.GetGenericTypeDefinition();
                        if (def == typeof(IRequestHandler<,>) || def == typeof(INotificationHandler<>))
                        {
                            services.AddTransient(iface, type);
                        }
                        else if (def == typeof(IPipelineBehavior<,>))
                        {
                            services.AddTransient(typeof(IPipelineBehavior<,>), type);
                        }
                    }
                }
            }
        }
    }
}
