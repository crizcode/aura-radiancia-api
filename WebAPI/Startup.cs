using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Presentacion
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configuración del CORS
            services.AddCors(options =>
            {
                options.AddPolicy("_myAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configurar un controlador de errores personalizado
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Redireccionar solicitudes HTTP a HTTPS
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Habilitar el middleware de CORS
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}