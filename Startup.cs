using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace TestAPIDotNetCore
{
        public class Startup
    {
        public IConfiguration Configurations { get; set; }
        public Startup(IWebHostEnvironment env)
        {
            System.Console.WriteLine(env.EnvironmentName);
            Configurations = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddControllers();
                //.AddNewtonsoftJson(options =>
                //{
                //    options.SerializerSettings.ContractResolver =new DefaultContractResolver();
                //}); 
            //services.AddControllers()
            //   .AddNewtonsoftJson(options =>
            //   {
            //        // Use the default property (Pascal) casing
            //      // options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //       options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //   });
            services.AddCors();
            services.AddDirectoryBrowser();
            services.AddRouting();
            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configurations);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BOANGAPI", Version = "v1" });
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseStaticFiles();

            //app.UseMvc();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.EnvironmentName != "Production")
            {
                string HostPrefix = Configurations["HostPrefix"].ToString();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(HostPrefix + "/swagger/v1/swagger.json", "Test API v1");
                });
            }
        }
    }





}
