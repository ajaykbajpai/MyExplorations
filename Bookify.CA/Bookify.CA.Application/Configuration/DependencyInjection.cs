using Bookify.CA.Domain.Bookings.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.CA.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        services.AddTransient<PricingService>();
        
        return services;
    }
}