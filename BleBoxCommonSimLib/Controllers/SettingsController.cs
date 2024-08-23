using BleBoxCommonSimLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BleBoxCommonSimLib.Controllers;

public class SettingsController(ISettingsService settings, ILogger<SettingsController> log) : ControllerBase
{
    [HttpGet("api/settings/state")]
    public IActionResult SettingsRequested()
    {
        try
        {
            return Ok(new { Settings = settings.ReadSettings() });
        }
        catch(Exception ex)
        {
            log.LogError("Error occurred during settings request: {ex}", ex);
            return BadRequest();
        }
    }

    [HttpPost("api/settings/set")]
    public IActionResult SettingsSetRequested(string request)
    {
        try
        {
            settings.UpdateSettings(request);
            return Ok(new { Settings = settings.ReadSettings() });
        }
        catch
        {
            return BadRequest();
        }
    }
}
