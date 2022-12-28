using ASP.Net.Core.Unity.Example.Extensions;
using ASP.Net.Core.Unity.Example.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Unity;
using Unity.Lifetime;
using Unity.Microsoft.DependencyInjection;

namespace ASP.Net.Unity.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication app = BuildWebApplication(args);
            ConfigureWebApplication(app);
            app.Run();
        }

        private static WebApplication BuildWebApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterInstance(new SomeClass(40));
            unityContainer.RegisterType(typeof(ILogger<>), typeof(FakeLogger<>), new TransientLifetimeManager());
            unityContainer.RegisterType(typeof(IService), typeof(Service), new TransientLifetimeManager());

            builder.Host.UseUnityServiceProvider(unityContainer, options =>
            {
                //By default implementations from Unity are overriden by ASP.NET implementation,
                //we can change it using PreferUnityImplementation method
                options.PreferUnityImplementation(typeof(ILogger<>));
                options.PreferUnityImplementation(typeof(ILogger));
            });

            builder.Services.AddMvc();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            //We can resolve custom injections at startup time, but they requires special actions that are implemented in extension method
            var serviceProvider = builder.Services.BuildCustomServiceProvider();
            var resolvedValue = serviceProvider.GetService<ILogger<SomeClass>>();

            return builder.Build();
        }

        private static void ConfigureWebApplication(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
