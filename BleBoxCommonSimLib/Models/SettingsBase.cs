using BleBoxCommonSimLib.Enums;

namespace BleBoxCommonSimLib.Models;

public record SettingsBase
{
    public string? DeviceName { get; set; }
    public Tunnel? Tunnel { get; set; }
    public StatusLed? StatusLed { get; set; }
}

public record StatusLed
{
    public Toggle Enabled { get; set; }
}

public record Tunnel
{
    public Toggle Enabled { get; set; }
    public Toggle? LogEnabled { get; set; }
}
