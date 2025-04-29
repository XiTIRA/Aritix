using System.Collections.Generic;
using Xitira.Aritix.Extensions;

namespace Xitira.Aritix.Input;

public class InputManager
{
    private KeyboardState _previousKeyboardState;
    private KeyboardState _currentKeyboardState;

    private GamePadState _previousGamePadState;
    private GamePadState _currentGamePadState;

    private MouseState _previousMouseState;
    private MouseState _currentMouseState;
    
    private GamePadState _previousVirtualGamePadState;
    private GamePadState _currentVirtualGamePadState;
    
    private TouchCollection _currentTouches;
    private TouchCollection _previousTouches;

    GamepadTypes _gamepadType = GamepadTypes.None;

    public void SetVirtualGamePadState(GamePadState state)
    {
        _previousVirtualGamePadState = _currentVirtualGamePadState;
        _currentVirtualGamePadState = state;
    }
    
    private Dictionary<string,HashSet<DigitalMapper>> _mappings = new Dictionary<string,HashSet<DigitalMapper>>();

    public InputManager()
    {
        _previousVirtualGamePadState = new GamePadState();
        _currentGamePadState = _previousVirtualGamePadState;
        
        _currentKeyboardState = Keyboard.GetState();
        _previousKeyboardState = _currentKeyboardState;

        _currentGamePadState = GamePad.GetState(PlayerIndex.One);
        _previousGamePadState = _currentGamePadState;

        _currentMouseState = Mouse.GetState();
        _previousMouseState = _currentMouseState;

        _previousTouches = _currentTouches;
        _currentTouches = TouchPanel.GetState();
    }
    
    public void AddDigitalMap(string name, DigitalMapper mapper)
    {
        if (_mappings.ContainsKey(name))
        {
            _mappings[name].Add(mapper);
        } else {
            _mappings.Add(name, new HashSet<DigitalMapper>{mapper});
        }
    }

    public void AddDigitalMap(string name, MouseButtons button)
    {
        var mapper = new DigitalMapper()
        {
            DigitalType = DigitalTypes.Mouse,
            DigitalIndex = (int)button
        };
        
        AddDigitalMap(name, mapper);
    }
    
    public void AddDigitalMap(string name, Buttons button)
    {
        var mapper = new DigitalMapper()
        {
            DigitalType = DigitalTypes.Gamepad,
            DigitalIndex = (int)button
        };
        
        AddDigitalMap(name, mapper);
    }

    public void AddDigitalMap(string name, Keys key)
    {
        var mapper = new DigitalMapper()
        {
            DigitalType = DigitalTypes.Keyboard,
            DigitalIndex = (int)key
        };
        
        AddDigitalMap(name, mapper);
    }
    
    public void RemoveDigitalMap(string name)
    {
        if (_mappings.ContainsKey(name))
        {
            _mappings.Remove(name);
        }
    }

    public void Update(GameTime gameTime)
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();

        _previousGamePadState = _currentGamePadState;
        _currentGamePadState = GamePad.GetState(PlayerIndex.One);
        
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();

        _currentTouches = TouchPanel.GetState();

        if (_currentGamePadState.IsConnected)
        {
            var capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            var name = capabilities.DisplayName;

            // _gamepadType = name.Contains("PS") ? GamepadTypes.DualShock : GamepadTypes.Xbox;
            // _gamepadType = name.Contains("Nintendo") ? GamepadTypes.Nintendo : _gamepadType;
        }
        else
        {
            _gamepadType = GamepadTypes.None;
        }

    }
    
    public void MapDefaultDirections()
    {
        AddDigitalMap("Up", Buttons.DPadUp);
        AddDigitalMap("Up", Keys.W);
        AddDigitalMap("Up", Keys.Up);
        AddDigitalMap("Up", Buttons.LeftThumbstickUp);
        
        AddDigitalMap("Down", Buttons.DPadDown);
        AddDigitalMap("Down", Keys.S);
        AddDigitalMap("Down", Keys.Down);
        AddDigitalMap("Down", Buttons.LeftThumbstickDown);
        
        AddDigitalMap("Left", Buttons.DPadLeft);
        AddDigitalMap("Left", Keys.A);
        AddDigitalMap("Left", Keys.Left);
        AddDigitalMap("Left", Buttons.LeftThumbstickLeft);
        
        AddDigitalMap("Right", Buttons.DPadRight);
        AddDigitalMap("Right", Keys.D);
        AddDigitalMap("Right", Keys.Right);
        AddDigitalMap("Right", Buttons.LeftThumbstickRight);
    }

    public void MapDefaultSkip()
    {
        AddDigitalMap("Skip", MouseButtons.Left);
        AddDigitalMap("Skip", Keys.Space);
        AddDigitalMap("Skip", Buttons.A);
        AddDigitalMap("Skip", Keys.Escape);
        AddDigitalMap("Skip", Keys.Enter);
    }
    
    public void MapDefaultBack()
    {
        AddDigitalMap("Back", MouseButtons.Right);
        AddDigitalMap("Back", Keys.Back);
        AddDigitalMap("Back", Keys.Escape);
        AddDigitalMap("Back", Buttons.Back);
        AddDigitalMap("Back", Buttons.Start);
        AddDigitalMap("Back",Buttons.RightShoulder);
        AddDigitalMap("Back",Buttons.RightTrigger);
    }
    
    public void MapDefaultJump()
    {
        AddDigitalMap("Jump", Keys.Space);
        AddDigitalMap("Jump", Buttons.A);
    }
    
    public bool Down(string map)
    {
        var down = false;

        foreach (var val in _mappings[map])
        {
            bool isDown;
            
            switch (val.DigitalType)
            {
                case DigitalTypes.Gamepad:
                    isDown = Down((Buttons)val.DigitalIndex);
                    break;
                case DigitalTypes.Keyboard:
                    isDown = Down((Keys)val.DigitalIndex);
                    break;
                case DigitalTypes.Mouse:
                    isDown = Down((MouseButtons)val.DigitalIndex);
                    break;
                default:
                    isDown = false;
                    break;
            }

            if (isDown)
            {
                down = true;
                break;
            }
        }

        return down;
    }
    
    public bool Up(string map)
    {
        var up = false;

        foreach (var val in _mappings[map])
        {
            bool isUp;
            
            switch (val.DigitalType)
            {
                case DigitalTypes.Gamepad:
                    isUp = Up((Buttons)val.DigitalIndex);
                    break;
                case DigitalTypes.Keyboard:
                    isUp = Up((Keys)val.DigitalIndex);
                    break;
                case DigitalTypes.Mouse:
                    isUp = Up((MouseButtons)val.DigitalIndex);
                    break;
                default:
                    isUp = false;
                    break;
            }

            if (isUp)
            {
                up = true;
                break;
            }
        }

        return up;
    }


    
    public bool Pressed(string map)
    {
        var pressed = false;

        foreach (var val in _mappings[map])
        {
            bool isPressed = false;
            
            switch (val.DigitalType)
            {
                case DigitalTypes.Gamepad:
                    isPressed = Pressed((Buttons)val.DigitalIndex);
                    break;
                case DigitalTypes.Keyboard:
                    isPressed = Pressed((Keys)val.DigitalIndex);
                    break;
                case DigitalTypes.Mouse:
                    isPressed = Pressed((MouseButtons)val.DigitalIndex);
                    break;

            }

            if (isPressed)
            {
                pressed = true;
                break;
            }
        }

        return pressed;
    }



    public Vector2 MouseScale(Vector2 worldSize, Vector2 screenSize)
    {
        var position = new Vector2();
        var mouse = _currentMouseState.Position.ToVector2();
        position.X = mouse.X.MapClamp(0, screenSize.X,0,worldSize.X);
        position.Y = mouse.Y.MapClamp(0, screenSize.Y,0,worldSize.Y);
        return position;
    }

    public Vector2 Axes(AxisTypes axis)
    {
        var pos = Vector2.Zero;

        switch (axis)
        {
            case AxisTypes.Mouse:
                pos = _currentMouseState.Position.ToVector2();
                break;
            case AxisTypes.LeftStick:
                pos = _currentGamePadState.ThumbSticks.Left;
                break;
            case AxisTypes.RightStick:
                pos = _currentGamePadState.ThumbSticks.Right;
                break;
            case AxisTypes.Touch :
                if (_currentTouches.Count > 0)
                    pos = _currentTouches[0].Position;
                break;
        }

        return pos;
    }

    public bool Pressed(MouseButtons button)
    {
        if (button == MouseButtons.Left)
        {
            if (_previousTouches.Count == 0 && _currentTouches.Count > 0) return true;
        }
        return Down(button) && !InternalMosueButtonState(button, _previousMouseState);
    }

    public bool Down(MouseButtons button)
    {
        return InternalMosueButtonState(button, _currentMouseState);
    }

    public bool Up(MouseButtons button)
    {
        return !InternalMosueButtonState(button, _currentMouseState);
    }

    private bool InternalMosueButtonState(MouseButtons button, MouseState mstate) => button switch
    {
        MouseButtons.Left => mstate.LeftButton == ButtonState.Pressed,
        MouseButtons.Middle => mstate.MiddleButton == ButtonState.Pressed,
        MouseButtons.Right => mstate.RightButton == ButtonState.Pressed,
        MouseButtons.X1 => mstate.XButton1 == ButtonState.Pressed,
        MouseButtons.X2 => mstate.XButton2 == ButtonState.Pressed,
        _ => false
    };

    public GamepadTypes GetPadType()
    {
        return _gamepadType;
    }

    public bool Pressed(Buttons button)
    {
        return (_currentGamePadState.IsButtonDown(button) && _previousGamePadState.IsButtonUp(button)) ||( _currentVirtualGamePadState.IsButtonDown(button) && _previousVirtualGamePadState.IsButtonUp(button));
    }

    public bool Down(Buttons button)
    {
        return _currentGamePadState.IsButtonDown(button) || _currentVirtualGamePadState.IsButtonDown(button);
    }

    public bool Up(Buttons button)
    {
        return _currentGamePadState.IsButtonUp(button) || _currentVirtualGamePadState.IsButtonUp(button);
    }

    public bool Pressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }

    public bool Down(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key);
    }

    public bool Up(Keys key)
    {
        return _currentKeyboardState.IsKeyUp(key);
    }
}