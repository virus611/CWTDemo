using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWTDemo.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CWTDemo
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

            services.AddCors(options =>
            {
                options.AddPolicy("any",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

            });
             
            services.AddMvc(option =>
            {
                option.ModelBinderProviders.Add(new CWTUserBinderProvider());
                option.Filters.Add(new CWTFilter());
            }).AddNewtonsoftJson(json =>
           {
               json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
               json.SerializerSettings.ContractResolver = new DefaultContractResolver();
               json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
           });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
             
            app.UseCors("any");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}
