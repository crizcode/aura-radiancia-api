using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSession();

        ConfigureCors(services);

    }
    private void ConfigureCors(IServiceCollection services)
    {
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aura Radiancia v1");

                c.OAuthClientId("swagger-client");
                c.OAuthAppName("Swagger Client");
                c.OAuthUsePkce();
            });
        }

        // Middleware para redirección HTTPS y servir archivos estáticos
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Middleware para el enrutamiento
        app.UseRouting();
        app.UseSession();

        // Middleware para habilitar la autenticación y la autorización
        app.UseAuthentication();
        app.UseAuthorization();

        // Agregar middleware personalizado JwtMiddleware
        app.UseMiddleware<JwtMiddleware>();

        // Middleware para habilitar CORS
        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        // Middleware para endpoints de controladores
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }


}

