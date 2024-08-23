using BleBoxCommonSimLib.Services;
using Microsoft.AspNetCore.Mvc;
using BleBoxCommonSimLib.Models;

namespace BleBoxCommonSimLib.Controllers;

public class NetworkController(INetworkService networkService, IDeviceInformationService deviceService) : ControllerBase
{
    [HttpGet("api/device/network")]
    public IActionResult NetworkInfoRequested()
    {
        try
        {
            var networkInformation = networkService.ReadNetworkInformation();
            return Ok(networkInformation);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("api/device/set")]
    public IActionResult SetAccessPointRequested(NetworkSetRequest request)
    {
        try
        {
            if (request.Network == null)
                return BadRequest();

            networkService.SetAccessPoint(request.Network);
            return Ok(new { Device = deviceService.ReadDeviceInformation(), Network = networkService.ReadNetworkInformation() });
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("api/wifi/scan")]
    public async Task<IActionResult> PerformWifiScan()
    {
        try
        {
            if (networkService.IsScanning)
                return StatusCode(409);

            return Ok(new { Ap = await networkService.WifiScan() });
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("api/wifi/connect")]
    public IActionResult PerformWifiConnect([FromBody] WifiConnectRequest request)
    {
        try
        {
            if (request.Ssid == null)
                return BadRequest();

            var wifiConnect = networkService.WifiConnect(request.Ssid, request.Pwd);

            return Ok(new { Ssid = wifiConnect.Item1, station_status = wifiConnect.Item2 });
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("api/wifi/disconnect")]
    public IActionResult PerformWifiDisconnect()
    {
        try
        {
            var wifiDisconnect = networkService.WifiDisconnect();

            return Ok(new { Ssid = wifiDisconnect.Item1, station_status = wifiDisconnect.Item2 });
        }
        catch
        {
            return BadRequest();
        }
    }
}
