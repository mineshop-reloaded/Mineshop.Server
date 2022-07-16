using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mineshop.Server.Application.Mappers;
using Mineshop.Server.Application.Options;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Payment.Creators;
using Mineshop.Server.Payment.Creators.Interfaces;
using Mineshop.Server.Payment.Handlers;
using Mineshop.Server.Service.Services;
using Mineshop.Server.Service.Services.Interfaces;
using Stripe;
using Swashbuckle.AspNetCore.SwaggerUI;
using ProductService = Mineshop.Server.Service.Services.ProductService;

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
        services.AddOptions();

        #region Mapper

        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile<ServerMapper>();
            configuration.AddProfile<CategoryMapper>();
            configuration.AddProfile<ProductMapper>();
            configuration.AddProfile<PaymentMapper>();
        });

        services.AddSingleton(mapperConfiguration.CreateMapper());

        #endregion

        #region Database Connection

        services.AddTransient<MineshopContext>();
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

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<SandboxPaymentHandler>();
        services.AddScoped<StripePaymentHandler>();

        services.AddSingleton<IDictionary<PaymentGateway, IPaymentCreator>>(
            new Dictionary<PaymentGateway, IPaymentCreator>
            {
                {
                    PaymentGateway.Sandbox, new SandboxPaymentCreator()
                },
                {
                    PaymentGateway.Stripe, new StripePaymentCreator()
                },
            });

        #endregion

        #region Swagger

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mineshop API",
                Version = "v1",
            });

            options.IncludeXmlComments(Path.Combine(
                AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
            ));
        });

        // Enum Names in Swagger UI
        services.AddControllersWithViews()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        #endregion

        #region StripeConfiguration

        var stripeConfiguration = Configuration.GetSection("Stripe");
        StripeConfiguration.ApiKey = stripeConfiguration["ApiKey"];

        services.Configure<StripeOptions>(options =>
        {
            options.WebhookSigningKey = stripeConfiguration["WebhookSigningKey"];
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
            options.DocExpansion(DocExpansion.None);
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