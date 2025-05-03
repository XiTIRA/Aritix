using Xitira.Aritix.Annex;
using Xitira.Aritix.Systems.Battery;

namespace Xitira.Aritix.Sdl;

public class SdlBatterySystem : IBatterySystem
{
    public BatteryInfo GetBatteryInfo()
    {
        var sdlState = Sdl.GetPowerInfo();

        BatteryStates state;
        switch (sdlState.State)
        {
            case SdlPowerState.Charged:
            case SdlPowerState.Charging:
                state = BatteryStates.Charging;
                break;
            case SdlPowerState.OnBattery:
                state = BatteryStates.Discharging;
                break;
            default:
                state = BatteryStates.NoBattery;
                break;
        }

        return new BatteryInfo()
        {
            Percent = sdlState.Percent/100f,
            State = state
        };
    }
}