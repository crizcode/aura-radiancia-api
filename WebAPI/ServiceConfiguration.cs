using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Domain.Repositories;
using Services.Abstractions;
using Services;
using Persistence;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infrastructure;

namespace Presentacion
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Registra AutoMapper
            services.AddAutoMapper(typeof(Startup), typeof(MappingProfile));

            // Registra IDbConnection
            services.AddScoped<IDbConnection>(provider =>
            {
                var connectionString = configuration.GetConnectionString("conn");
                return new SqlConnection(connectionString);
            }); ;

            // Registra los servicios
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
  
        }
    }
}
