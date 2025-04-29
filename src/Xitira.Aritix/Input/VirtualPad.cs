using Xitira.Aritix.Extensions;
using Xitira.Aritix.Graphic;

namespace Xitira.Aritix.Input;

public class VirtualPad
{
    private Texture2D _padBack;
    private Texture2D _padFront;
    private Atlas _buttons;

    private Vector2 _homePosition;
    private Vector2 _backOrigin;
    private Vector2 _frontOrigin;

    private float _scale = .45f;
    private float _opacity = .5f;

    private GamePadState _gs = new();
    
    public Vector2 _stickPosition = Vector2.Zero;
    public Vector2 _stickPull = Vector2.Zero;

    public VirtualPad(Texture2D padBack, Texture2D padFront, Atlas buttons, Vector2 position)
    {
        _padBack = padBack;
        _padFront = padFront;
        _homePosition = position;
        _buttons = buttons;

        _buttons.SetScale(_scale);
        _buttons.SetOpacity(_opacity);
        _buttons.SetOrigin(Origins.Center);
        
        _backOrigin = new Vector2(_padBack.Width / 2.0f, _padBack.Height / 2.0f);
        _frontOrigin = new Vector2(_padFront.Width / 2.0f, _padFront.Height / 2.0f);
    }

    public void Update(GameTime gt, InputManager inputManager)
    {
        var pos = inputManager.Axes(AxisTypes.Mouse);
        var leftClick = inputManager.Down(MouseButtons.Left);
        var relative = pos - _homePosition;

        _stickPull = Vector2.Zero;

        float maxStickDistance = 80.0f * _scale;
        if (leftClick && relative.Length() < maxStickDistance)
        {
            _stickPull = relative;
        }
        relative.X = _stickPull.X.MapClamp(-maxStickDistance, maxStickDistance, -1.0f, 1.0f);
        relative.Y = _stickPull.Y.MapClamp(-maxStickDistance, maxStickDistance, -1.0f, 1.0f);
        relative.Y = -relative.Y;
        
        var buttons = Buttons.None;
        buttons |= Buttons.A;
        buttons |= Buttons.B;
        
        
        _gs = new GamePadState(
            new GamePadThumbSticks(relative, new Vector2()),
            new GamePadTriggers(0f, 0f),
            new GamePadButtons(buttons),
            new GamePadDPad(0, 0, 0, 0));
        
        inputManager.SetVirtualGamePadState(_gs);
    }
    
    public GamePadState GamePadState => _gs;

    public void Draw(GameTime gt, SpriteBatch sb)
    {
        var offset = new Vector2(150, 470); 
        
        _buttons.Draw(sb, 0, offset +new Vector2(-30, 0));
        _buttons.Draw(sb, 1, offset +new Vector2(30,0));
        _buttons.Draw(sb, 2, offset +new Vector2(0, -30));
        _buttons.Draw(sb, 3, offset +new Vector2(0, 30));

         offset = new Vector2(250, 470); 
        
        _buttons.Draw(sb, 4, offset +new Vector2(-30, 0));
        _buttons.Draw(sb, 5, offset +new Vector2(30,0));
        _buttons.Draw(sb, 6, offset +new Vector2(0, -30));
        _buttons.Draw(sb, 7, offset +new Vector2(0, 30));
        
        sb.Draw(
            _padBack,
            _homePosition,
            null,
            Color.White * _opacity,
            0.0f,
            _backOrigin,
            _scale,
            SpriteEffects.None,
            0.0f);

        sb.Draw(
            _padFront,
            _homePosition + _stickPull,
            null,
            Color.White * _opacity,
            0.0f,
            _frontOrigin,
            _scale,
            SpriteEffects.None,
            0.0f);
    }
}