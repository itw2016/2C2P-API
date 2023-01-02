using Dapper.WebApi.Handlers;
using Dapper.WebApi.Handlers.Implementation;
using Dapper.WebApi.Services;
using Dapper.WebApi.Services.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;

namespace Dapper.WebApi
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
            services.AddTransient<ICommandText, CommandText>();
            services.AddTransient<ITransRepository, TransRepository>();
            services.AddTransient<IExtractHandler, ExtractHandler>();
            services.AddMvc();
            services.AddCors();

            services.AddOpenApiDocument(config =>
            {
                config.Title = "ASPNET CORE 3.1/WebAPI/Dapper Async/MSSQL Prototype";
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
                    //.SetIsOriginAllowed(origin => true) // allow any origin
                    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    //.AllowCredentials()); // allow credentials

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
