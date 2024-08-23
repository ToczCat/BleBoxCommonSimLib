using BleBoxModels.Common.Models;
using BleBoxCommonSimLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace BleBoxCommonSimLib.Controllers;

public class InformationController(IDeviceInformationService deviceInformationService) : ControllerBase
{
    [HttpGet("info")]
    public IActionResult DeviceInfoRequested()
    {
        try
        {
            var deviceInformation = deviceInformationService.ReadDeviceInformation();

            return Ok(new { Device = deviceInformation });
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("api/device/uptime")]
    public IActionResult DeviceUptimeRequested()
    {
        try
        {
            var deviceUptime = deviceInformationService.ReadUptime();

            return Ok(new { UpTimeS = ((int)deviceUptime.TotalSeconds).ToString() });
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("api/ota/update")]
    public async Task<IActionResult> FirmwareUpdateRequested(SettingsBase Settings)
    {
        try
        {
            if (deviceInformationService.IsUpdating)
                return StatusCode(409);

            await deviceInformationService.PerformFirmwareUpdate();

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
