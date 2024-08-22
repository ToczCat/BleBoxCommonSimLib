namespace BleBoxCommonSimLib.Models;

public record Device
{
    public string? DeviceName { get; set; }
    public string? Product { get; set; }
    public string? Type { get; set; }
    public string? ApiLevel { get; set; }
    public string? Hv { get; set; }
    public string? Fv { get; set; }
    public string? Id { get; set; }
    public string? Ip { get; set; }
}
