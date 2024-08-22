using BleBoxCommonSimLib.Enums;
using System.Text.Json.Serialization;

namespace BleBoxCommonSimLib.Models;

public record Network
{
    public string? Ssid { get; set; }
    public string? Bssid { get; set; }
    public string? Ip { get; set; }
    public string? Mac { get; set; }

    [JsonPropertyName("station_status")]
    public StationStatus StationStatus { get; set; }

    [JsonPropertyName("tunnel_status")]
    public TunnelStatus TunnelStatus { get; set; }

    public bool ApEnable { get; set; }
    public string? ApSSID { get; set; }
    public string? ApPasswd { get; set; }
    public int Channel { get; set; }
}
