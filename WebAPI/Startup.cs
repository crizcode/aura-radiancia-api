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

    // Método para configurar servicios
    public void ConfigureServices(IServiceCollection services)
    {
        // Configuración de servicios necesarios para la aplicación
        services.AddControllers();
        services.AddSession();

        // JwtMiddleware
        // services.AddScoped<JwtMiddleware>();

        // Configuración del CORS
        ConfigureCors(services);

        // Configuración de la autenticación JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
    // Método para configurar CORS
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

    // Método para configurar la aplicación y sus middlewares
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Middleware para página de error detallada en entorno de desarrollo
            app.UseDeveloperExceptionPage();

            // Middleware para servir Swagger UI en entorno de desarrollo
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aura Radiancia v1");

                // Configuración del botón "Authorize" en Swagger UI
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

