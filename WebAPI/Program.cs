using Presentacion;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios necesarios para la aplicaci�n
// Agrega servicios necesarios para la aplicaci�n
ServiceConfiguration.Configure(builder.Services, builder.Configuration);
builder.Services.AddControllers();

// Agrega servicios requeridos para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura la conexi�n a la base de datos usando Dapper
builder.Services.AddScoped<IDbConnection>(c =>
    new SqlConnection(builder.Configuration.GetConnectionString("conn")));

var app = builder.Build();

// Configura el middleware de la aplicaci�n
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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();