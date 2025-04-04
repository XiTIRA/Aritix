using System;
using System.ComponentModel.Design;

namespace Aritix.Gui;

public class Button : IDisposable
{
    public Texture2D Hover;
    public Texture2D Pressed;
    public Texture2D Released;
    public Texture2D Current;
    
    public Vector2 Position;
    public Rectangle Bounds;
    
    public Action OnClick;

    public bool IsContinuous;
    public bool ClickedLastFrame = false;

    public bool IsPressed = false;
    public bool IsClicked = false;

    public Button(Texture2D hover, Texture2D pressed, Texture2D released, Vector2 position, bool Continuous)
    {
        Hover = hover;
        Pressed = pressed;
        Released = released;
        Position = position;

        Current = released;
        IsContinuous = Continuous;
        
        Bounds = new Rectangle((int)position.X, (int)position.Y, hover.Width, hover.Height);
    }

    public void Attach(Action onClick)
    {
        OnClick += onClick;
    }

    public void Dispose()
    {
        OnClick = null;
        Hover = null;
        Pressed = null;
        Released = null;
        Current = null;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Current, Position, Color.White);
    }

    public void Update(GameTime gt, Vector2 mousePosition, bool pressed)
    {
        IsClicked = false;
        IsPressed = false;
        if (Bounds.Contains(mousePosition))
        {
            if (pressed)
            {
                Current = Pressed;
                IsPressed = true;

                if (IsContinuous)
                {
                    OnClick?.Invoke();
                } else if (!ClickedLastFrame)
                {
                    IsClicked = true;
                    OnClick?.Invoke();
                }
                
            }
            else
            {
                Current = Hover;
            }
            
        }
        else
        {
            Current = Released;
        }          
        ClickedLastFrame = IsPressed;

    }
}