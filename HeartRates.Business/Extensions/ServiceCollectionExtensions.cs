using HeartRates.Business.Actions;
using HeartRates.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HeartRates.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IHeartRateService, HeartRateService>()
            .AddScoped<ITrackAccessAction, TrackAccessAction>()
            .AddScoped<IHeartRateRepository, HeartRateRepository>();
    }

    public static IServiceCollection AddDatabaseSupport(this IServiceCollection services)
    {
        //For development only;
        //simulates registering the database context accessor (and in this case also pre-fetches dummy data);
        return services
            .AddSingleton<DbContextDataset>(_ => DbContextDataset.Load());
    }
}