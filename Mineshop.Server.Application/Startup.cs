using AutoMapper;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mineshop.Server.Application.Mappers;
using Mineshop.Server.Service.Services;
using Mineshop.Server.Service.Services.Interfaces;

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
        #region Services

        services.AddControllers();

        #endregion

        #region Mapper

        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile<ServerMapper>();
        });

        var mapper = mapperConfiguration.CreateMapper();
        services.AddSingleton(mapper);

        #endregion

        #region Database Connection

        services.AddDbContext<MineshopContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("Default"));
        });

        #endregion

        #region Dependency Injection

        services.AddScoped<IServerService, ServerService>();
        services.AddScoped<IServerRepository, ServerRepository>();

        #endregion

        #region Swagger

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mineshop API",
                Version = "v1"
            });
        });

        #endregion
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment()) application.UseDeveloperExceptionPage();

        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mineshop API");
        });

        application.UseRouting();

        application.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}