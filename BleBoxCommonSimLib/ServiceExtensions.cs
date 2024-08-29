using BleBoxCommonSimLib.Services;
using BleBoxModels.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Nodes;

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

    public static IServiceProvider ConfigureCommonSim(
        this IServiceProvider services,
        string deviceName,
        string type,
        string apiLevel,
        Func<SettingsBase, object> obtainFullSettings,
        Func<JsonObject, SettingsBase> updateFullSettings,
        string product = "commonBox")
    {
        var deviceService = services.GetRequiredService<IDeviceInformationService>();
        deviceService.InitializeDevice(deviceName, type, apiLevel, product);

        var settingsService = services.GetRequiredService<ISettingsService>();

        settingsService.ObtainFullSettings = obtainFullSettings;
        settingsService.UpdateFullSettings = updateFullSettings;

        return services;
    }
}
