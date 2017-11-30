using DomainVsContextualValidation.Core;
using DomainVsContextualValidation.Data;
using DomainVsContextualValidation.Web.Filters;
using DomainVsContextualValidation.Web.Requests;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using RequestInjector.NetCore;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace DomainVsContextualValidation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IRequest), typeof(AddUserForFinancialAppRequest))
                .AddClasses()
                .AsSelf()
                .WithTransientLifetime());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Domain Vs Contextual Validation API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "DomainVsContextualValidation.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            var provider = services.BuildServiceProvider();

            services.AddMvc(config =>
            {
                config.ModelMetadataDetailsProviders.Add(new RequestInjectionMetadataProvider());
                config.ModelBinderProviders.Insert(0, new QueryModelBinderProvider(provider));
                config.Filters.Add(new ValidationFilter());
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new RequestInjectionHandler<IRequest>(provider));
            })
            .AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssemblyContaining<UserValidator>();
                c.RegisterValidatorsFromAssemblyContaining<AddUserForFinancialAppRequest>();
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Validation API Example");
                    c.ShowRequestHeaders();
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
