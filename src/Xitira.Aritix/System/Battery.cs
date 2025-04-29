using Xitira.Aritix.Extensions;

namespace Xitira.Aritix.System;

public class Battery
{
    private Texture2D Full;
    private Texture2D Empty;
    private Texture2D Half;
    private Texture2D Charging;
    private Texture2D CurrentTexture;

    Vector2 Position = Vector2.Zero;
    Vector2 Origin = Vector2.Zero;

    private double UpdateSeconds;
    private double _elapsed;

    bool isCharging = false;
    private float percent = 0;

    public Battery(Texture2D full, Texture2D empty, Texture2D half, Texture2D charging, Vector2 position, double updateSeconds)
    {
        Full = full;
        Empty = empty;
        Half = half;
        Charging = charging;
        UpdateSeconds = updateSeconds;
        _elapsed = 0;
        Position = position;
        Origin = new Vector2(Charging.Width / 2, Charging.Height / 2);

        Update();
    }

    public void Update()
    {
        _elapsed = 0;

        var status = global::Xitira.Aritix.Extensions.Sdl.GetPowerInfo();
        // var status = new SdlPowerInfo()
        // {
        //     Percent = -1,
        //     Seconds = -1,
        //     State = SdlPowerState.NoBattery
        // };

        isCharging = status.State == SdlPowerState.Charging || status.State == SdlPowerState.NoBattery;
        percent = status.Percent;

        if (isCharging) CurrentTexture = Charging;
        else if (percent > .8) CurrentTexture = Full;
        else if (percent < .3) CurrentTexture = Half;
        else CurrentTexture = Empty;
    }

    public void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsed >= UpdateSeconds)
        {
            Update();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            CurrentTexture,
            Position,
            null,
            Color.White,
            0f,
            Origin,
            1f,
            SpriteEffects.None,
            0f
        );
    }
}