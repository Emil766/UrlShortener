using Autofac;
using System.Linq;
using System.Reflection;

namespace UrlShortener.Extensions
{
    public static class AutofacExtensions
    {
        public static void AssemblyScanning(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Services.Interfaces"), Assembly.Load("Services.Implementations"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("Repository.Interfaces"), Assembly.Load("Repository.MongoDB"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }
    }
}
