using BleBoxModels.Common.Models;
using System.Net;

namespace BleBoxCommonSimLib.Services;

public interface IDeviceInformationService
{
    bool IsUpdating { get; }
    string DeviceName { get; set; }

    void InitializeDevice(string deviceName, string type, string apiLevel, string product = "commonBox");
    Device ReadDeviceInformation();
    TimeSpan ReadUptime();
    Task PerformFirmwareUpdate();
}

public class DeviceInformationService : IDeviceInformationService
{
    private string _product = "commonBox";
    private string _type = "commonBox";
    private string _apiLevel = "20230606";
    private string _hv = "9.1d";
    private double _fv = 0.987;
    private string _id = Guid.NewGuid().ToString();
    private string _ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
    private DateTime _createdDateTime = DateTime.Now;

    public bool IsUpdating { get; private set; } = false;
    public string DeviceName { get; set; } = "My BleBox device name";

    public void InitializeDevice(string deviceName, string type, string apiLevel, string product = "commonBox")
    {
        DeviceName = deviceName;
        _product = product;
        _type = type;
        _apiLevel = apiLevel;
    }

    public Device ReadDeviceInformation()
    {
        var device = new Device
        {
            DeviceName = DeviceName,
            Product = _product,
            Type = _type,
            ApiLevel = _apiLevel,
            Hv = _hv,
            Fv = _fv.ToString(),
            Id = _id,
            Ip = _ip
        };

        return device;
    }

    public TimeSpan ReadUptime()
    {
        return _createdDateTime - DateTime.Now;
    }

    public async Task PerformFirmwareUpdate()
    {
        IsUpdating = true;
        _fv += 0.001;

        await Task.Delay(TimeSpan.FromSeconds(20));

        IsUpdating = false;
    }
}
