using System;
using System.Collections.Generic;
using System.Linq;
using Flagger.Core;
using Flagger.Service;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Flagger
{
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));

            services.UseSimpleInjectorAspNetRequestScoping(_container);


            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(GetUsers());
        }

        private List<TestUser> GetUsers()
        {
            return _container.GetInstance<IUserGateway>().Get()
                .Select(s => new TestUser
                {
                    SubjectId = s.Id_User.ToString(),
                    Username = s.UserName,
                    Password = s.Password
                })
                .ToList();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeContainer(app);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:63518",
                RequireHttpsMetadata = false,
                ApiName = "flagger"
            });

            app.UseMvc();

        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            _container.RegisterSingleton<IFlagGateway>(() => new FlagGateway(Configuration.GetConnectionString("FlaggerDb")));
            _container.RegisterSingleton<IUserGateway>(() => new UserGateway(Configuration.GetConnectionString("FlaggerDb")));
            _container.RegisterSingleton<IConfigurationGateway>(() => new ConfigurationGateway(Configuration.GetConnectionString("FlaggerDb")));

            _container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());

            _container.RegisterSingleton<Func<IViewBufferScope>>(
                () => app.GetRequestService<IViewBufferScope>());
        }
    }
}
