namespace Xitira.Aritix.Systems.Battery;

public class NullBatterySystem : IBatterySystem
{
    public BatteryInfo GetBatteryInfo()
    {
        return new BatteryInfo()
        {
            Percent = 0,
            State = BatteryStates.NoBattery
        };
    }
}