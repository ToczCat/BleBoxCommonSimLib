using BleBoxModels.Common.Enums;
using BleBoxModels.Common.Models;
using System.Net;

namespace BleBoxCommonSimLib.Services;

public interface INetworkService
{
    bool IsScanning { get; }

    Network ReadNetworkInformation();
    void SetAccessPoint(NetworkSet settings);
    (string, StationStatus) WifiConnect(string ssid, string? password);
    (string, StationStatus) WifiDisconnect();
    Task<AccessPoint[]> WifiScan();
}

public class NetworkService : INetworkService
{
    private string _ssid = "Wifi_name";
    private string _bssid = "70:4f:25:24:11:ae";
    private string _ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
    private string _mac = "bb:50:ec:2d:22:17";
    private StationStatus _stationStatus = StationStatus.Connected;
    private TunnelStatus _tunnelStatus = TunnelStatus.Connected;
    private bool _apEnable = true;
    private string _apSSID = "commonBox-g650e32d2217";
    private string _apPasswd = "my_secret_password";
    private int _channel = 7;
    private Random _rnd = new();

    public bool IsScanning { get; private set; } = false;

    public Network ReadNetworkInformation()
    {
        return new Network
        {
            Ssid = _ssid,
            Bssid = _bssid,
            Ip = _ip,
            Mac = _mac,
            StationStatus = _stationStatus,
            TunnelStatus = _tunnelStatus,
            ApEnable = _apEnable,
            ApSSID = _apSSID,
            ApPasswd = _apPasswd,
            Channel = _channel
        };
    }

    public void SetAccessPoint(NetworkSet settings)
    {
        _apEnable = settings.ApEnable;
        _apSSID = settings.ApSSID ?? string.Empty;
        _apPasswd = settings.ApPasswd ?? string.Empty;
    }

    public async Task<AccessPoint[]> WifiScan()
    {
        IsScanning = true;

        await Task.Delay(TimeSpan.FromSeconds(10));

        IsScanning = false;

        return
            [
                new AccessPoint
                    { Ssid = "Funny_WiFi_Name", Rssi = _rnd.Next(-90, 0), Enc = EncryptionMode.WPA2 },
                new AccessPoint
                    { Ssid = "Less_Funny_WiFi_Name", Rssi = _rnd.Next(-90, 0), Enc = EncryptionMode.WPA_WPA2 },
                new AccessPoint
                    { Ssid = "Not_Funny_WiFi_Name", Rssi = _rnd.Next(-90, 0), Enc = EncryptionMode.OpenNetwork }
            ];
    }

    public (string, StationStatus) WifiConnect(string ssid, string? password)
    {
        _ssid = ssid;
        _stationStatus = StationStatus.Connected;

        return (_ssid, _stationStatus);
    }

    public (string, StationStatus) WifiDisconnect()
    {
        _stationStatus = StationStatus.NotConfigured;

        return (_ssid, _stationStatus);
    }
}
