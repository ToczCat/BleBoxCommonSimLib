namespace BleBoxCommonSimLib.Models;

public record NetworkSet
{
    public bool ApEnable { get; set; }
    public string? ApSSID { get; set; }
    public string? ApPasswd { get; set; }
}
