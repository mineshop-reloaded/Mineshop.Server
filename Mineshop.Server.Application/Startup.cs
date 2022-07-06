using System.Reflection;
using AutoMapper;
using Infrastructure.Context;
using Infrastructure.Repositories.Category;
using Infrastructure.Repositories.Interfaces.Category;
using Infrastructure.Repositories.Interfaces.Server;
using Infrastructure.Repositories.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mineshop.Server.Application.Mappers;
using Mineshop.Server.Service.Services.Category;
using Mineshop.Server.Service.Services.Interfaces.Category;
using Mineshop.Server.Service.Services.Interfaces.Server;
using Mineshop.Server.Service.Services.Server;

namespace Mineshop.Server.Application;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        #region Mapper

        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile<ServerMapper>();
            configuration.AddProfile<CategoryMapper>();
        });

        services.AddSingleton(mapperConfiguration.CreateMapper());

        #endregion

        #region Database Connection

        services.AddDbContext<MineshopContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("Default"));
        });

        #endregion

        #region Dependency Injection

        services.AddScoped<IServerRepository, ServerRepository>();
        services.AddScoped<IServerService, ServerService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        #endregion

        #region Swagger

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mineshop API",
                Version = "v1"
            });

            options.IncludeXmlComments(Path.Combine(
                AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
            ));
        });

        #endregion
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment()) application.UseDeveloperExceptionPage();

        #region Swagger

        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mineshop API");
            options.RoutePrefix = string.Empty;
        });

        #endregion

        #region Routing

        application.UseRouting();
        application.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        #endregion
    }
}