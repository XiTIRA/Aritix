using Xitira.Aritix.Annex;

namespace Xitira.Aritix.Systems.Battery;

public struct BatteryInfo
{
    public float Percent { get; set; }
    public BatteryStates State { get; set; }
}