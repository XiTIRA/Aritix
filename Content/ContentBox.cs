using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Aritix.Content;

public class ContentBox : IDisposable
{
    private readonly ContentLoader _loader;
    private Dictionary<string, Texture2D> _textures = new();
    private Dictionary<string, SoundEffect> _sounds = new();
    
    public ContentBox(ContentLoader loader)
    {
        _loader = loader;
    }

    public Texture2D GetSprite(string path)
    {
        if (!_textures.TryGetValue(path, out var texture))
        {
            texture = _loader.GetSprite(path);
            _textures.Add(path, texture);
        }
        return texture;
    }

    public SoundEffect GetSound(string path)
    {
        if (!_sounds.TryGetValue(path, out var sound))
        {
            sound = _loader.GetSound(path);
            _sounds.Add(path, sound);
        }
        return sound;
    }

    public void Dispose()
    {
        _textures.Clear();
        _textures = null;
        _sounds.Clear();
        _sounds = null;
    }
}