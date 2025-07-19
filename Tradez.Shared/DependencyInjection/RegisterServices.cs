using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Tradez.Shared.DependencyInjection;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TransientServiceAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class)]
public sealed class ScopedServiceAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class)]
public sealed class SingletonServiceAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class RegisterInAttribute(params string[] environments) : Attribute
{
    public string[] Environments { get; } = environments;
}

public static class ServiceRegistrationHelper
{
    public static void AddAttributedServices(this IServiceCollection services, Assembly assembly, string? @namespace = null, IHostEnvironment? env = null, ILogger? logger = null)
    {
        var allTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes().Any());

        if (!string.IsNullOrEmpty(@namespace))
            allTypes = allTypes.Where(t => t.Namespace != null && t.Namespace.StartsWith(@namespace));

        foreach (var implType in allTypes)
        {
            var lifetime = GetServiceLifetime(implType);
            if (lifetime == null) continue;

            var registerInAttr = implType.GetCustomAttribute<RegisterInAttribute>();
            if (registerInAttr != null && env is not null)
            {
                if (!registerInAttr.Environments.Contains(env.EnvironmentName, StringComparer.OrdinalIgnoreCase))
                    continue;
            }

            var interfaces = implType.GetInterfaces().Where(i => !i.FullName!.StartsWith("System")).ToArray();
            if (!interfaces.Any()) interfaces = new[] { implType };

            foreach (var iface in interfaces)
            {
                if (!services.Any(sd => sd.ServiceType == iface && sd.ImplementationType == implType))
                {
                    services.Add(new ServiceDescriptor(iface, implType, lifetime.Value));
                    logger?.LogInformation("Registered {Impl} as {Interface} [{Lifetime}]", implType.Name, iface.Name, lifetime);
                }
            }

            if (implType.IsGenericTypeDefinition && interfaces.Any(i => i.IsGenericTypeDefinition))
            {
                foreach (var openInterface in interfaces.Where(i => i.IsGenericTypeDefinition))
                {
                    services.Add(new ServiceDescriptor(openInterface, implType, lifetime.Value));
                    logger?.LogInformation("Registered open generic {Impl} as {Interface} [{Lifetime}]", implType.Name, openInterface.Name, lifetime);
                }
            }
        }
    }

    private static ServiceLifetime? GetServiceLifetime(Type type)
    {
        if (type.GetCustomAttribute<TransientServiceAttribute>() != null)
            return ServiceLifetime.Transient;
        if (type.GetCustomAttribute<ScopedServiceAttribute>() != null)
            return ServiceLifetime.Scoped;
        if (type.GetCustomAttribute<SingletonServiceAttribute>() != null)
            return ServiceLifetime.Singleton;
        return null;
    }
}
