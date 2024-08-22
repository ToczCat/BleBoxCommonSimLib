using BleBoxCommonSimLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace BleBoxCommonSimLib.Controllers;

public class SettingsController(ISettingsService settings) : ControllerBase
{
    [HttpGet("api/settings/state")]
    public IActionResult SettingsRequested()
    {
        try
        {
            return Ok(new { Settings = settings.ReadSettings() });
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost("api/settings/set")]
    public IActionResult SettingsSetRequested(object request)
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
