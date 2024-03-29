using Core.Helpers;
using Presentacion;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration.Configure(builder.Services, builder.Configuration);
builder.Services.AddControllers();

// Servicios mapea AppSettings

//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Agrega servicios requeridos para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura la conexión a la base de datos usando Dapper
builder.Services.AddScoped<IDbConnection>(c =>
    new SqlConnection(builder.Configuration.GetConnectionString("conn")));



var app = builder.Build();


// Configura el middleware de la aplicación
if (app.Environment.IsDevelopment())
{
    // Habilita el middleware para servir Swagger generado como un punto final JSON.
    app.UseSwagger();

    // Habilita el middleware para servir swagger-ui (HTML, JS, CSS, etc.),
    // especificando el punto final JSON de Swagger.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aura Radiancia v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();


// Habilitar CORS
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Agregar el middleware JwtMiddleware a la cadena de middlewares
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
