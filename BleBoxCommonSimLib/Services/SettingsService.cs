using BleBoxModels.Common.Enums;
using BleBoxModels.Common.Models;

namespace BleBoxCommonSimLib.Services;

public interface ISettingsService
{
    Func<SettingsBase, object>? ObtainFullSettings { get; set; }
    Func<object, SettingsBase>? UpdateFullSettings { get; set; }

    object ReadSettings();
    void UpdateSettings(object request);
}

public class SettingsService(DeviceInformationService deviceInformation) : ISettingsService
{
    private Toggle _tunnelEnabled = Toggle.Enabled;
    private Toggle? _tunnelLogEnabled = Toggle.Enabled;
    private Toggle _statusLedEnabled = Toggle.Enabled;

    public Func<SettingsBase, object>? ObtainFullSettings { get; set; }
    public Func<object, SettingsBase>? UpdateFullSettings { get; set; }

    public object ReadSettings()
    {
        if (ObtainFullSettings == null)
            throw new Exception("Full settings func is null");

        var settings = ObtainFullSettings(
            new SettingsBase
            {
                DeviceName = deviceInformation.DeviceName,
                Tunnel = new Tunnel
                {
                    Enabled = _tunnelEnabled,
                    LogEnabled = _tunnelLogEnabled
                },
                StatusLed = new StatusLed
                {
                    Enabled = _statusLedEnabled
                }
            });

        return settings;
    }

    public void UpdateSettings(object request)
    {
        if (UpdateFullSettings == null)
            throw new Exception("Full settings func is null");

        var settings = UpdateFullSettings(request);

        _tunnelEnabled = settings.Tunnel?.Enabled ?? Toggle.Disabled;
        _tunnelLogEnabled = settings.Tunnel?.LogEnabled;
        _statusLedEnabled = settings.StatusLed?.Enabled ?? Toggle.Disabled;
        deviceInformation.DeviceName = settings.DeviceName ?? string.Empty;
    }
}
