using DevIO.Api.Extensions;
using DevIO.Business.Interfaces;
using DevIO.Business.Notifications;
using DevIO.Business.Services;
using DevIO.Data.Context;
using DevIO.Data.Repository;

namespace DevIO.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<MyDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<IProviderService, ProviderService>();
        services.AddScoped<IProductService, ProductService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

        return services;
    }
}
