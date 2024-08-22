namespace BleBoxCommonSimLib.Models;

public record WifiConnectRequest
{
    public string? Ssid { get; set; }
    public string? Pwd { get; set; }
}
