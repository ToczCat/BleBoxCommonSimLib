using BleBoxModels.Common.Models;

namespace BleBoxCommonSimLib.Models;

public record NetworkSetRequest
{
    public NetworkSet? Network { get; set; }
}
