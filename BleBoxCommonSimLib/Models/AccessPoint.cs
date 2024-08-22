using BleBoxCommonSimLib.Enums;

namespace BleBoxCommonSimLib.Models;

public record AccessPoint
{
    public string? Ssid { get; set; }
    public int Rssi { get; set; }
    public EncryptionMode Enc { get; set; }
}

