using BleBoxCommonSimLib.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BleBoxCommonSimLib;

public static class ServiceExtensions
{
    public static IServiceCollection AddCommonSim(this IServiceCollection services)
    {
        services
            .AddSingleton<IDeviceInformationService, DeviceInformationService>()
            .AddSingleton<INetworkService, NetworkService>()
            .AddSingleton<ISettingsService, SettingsService>();

        return services;
    }
}
