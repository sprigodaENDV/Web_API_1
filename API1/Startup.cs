using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using API1.Persistence.Entities;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<LocalDBContext>(ServiceLifetime.Scoped);

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                //options.ApiVersionReader = ApiVersionReader.Combine(
                //                                                    new HeaderApiVersionReader("headerapiversion"),
                //                                                    new MediaTypeApiVersionReader("contentapiversion"),
                //                                                    new QueryStringApiVersionReader("queryapiversion")
                //                                                    );
            });

            //Without this won't even show  Swagger error page

            services.AddVersionedApiExplorer(setup =>
            {
                //setup.SubstitutionFormat = 
                //setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            //SwaggerOptions

            //services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

            //SwaggerOptions0

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //services.AddSwaggerGen(g =>
            //{
            //    var provider = services.BuildServiceProvider();
            //    var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();
            //    foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
            //    {
            //        //g.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
            //        g.SwaggerDoc(description.ApiVersion.ToString(), new Microsoft.OpenApi.Models.OpenApiInfo
            //        {
            //            Title = "API1",
            //            Description = description.ApiVersion.ToString(),
            //            //Description = description.GroupName,
            //            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            //            {
            //                Name = "Stanislav Prigoda",
            //                Email = "Stanislav.Prigoda@Endava.com"
            //            }
            //        });
            //    }
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseStatusCodePages();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            //app.UseSwagger(r => r.RouteTemplate = "/swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(e =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    //e.SwaggerEndpoint($"swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    e.SwaggerEndpoint($"swagger/{description.ApiVersion.ToString()}/swagger.json", description.ApiVersion.ToString().ToUpperInvariant());
                    //e.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    //e.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                }

                //With this shows Swagger error page

                e.RoutePrefix = "swagger";
                //e.RoutePrefix = "";
                //e.RoutePrefix = string.Empty;
            });
        }
    }
}
