using Core.Domain.Interfaces;
using Core.Services;
using Core.Services.Abstractions;
using Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using Persistence.Mapping;
using System.Data;
using System.Data.SqlClient;


namespace Presentacion
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Registra AutoMapper
            services.AddAutoMapper(typeof(Startup), typeof(MappingProfile));

            // Configura Swagger y la seguridad JWT
            ConfigureSwagger(services);

            // Registra IDbConnection
            services.AddScoped<IDbConnection>(provider =>
            {
                var connectionString = configuration.GetConnectionString("conn");
                return new SqlConnection(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aura Radiancia API", Version = "v1" });

                // Configurar la seguridad para JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Introduce tu token JWT en este formato: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

        }

    }
}
