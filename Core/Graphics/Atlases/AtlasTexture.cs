using Microsoft.Xna.Framework;

namespace Stella.Core.Graphics.Atlases;

/// <summary>
/// Represents a texture on an <see cref="Stella.Core.Graphics.Atlases.Atlas"/>. Contains its position on the atlas, and a unique string identifier.<br/>
/// Use <see cref="AtlasManager.GetTexture(string)"/> to retrieve an instance with the given string identifier.
/// </summary>
public record AtlasTexture(string Name, Atlas Atlas, Rectangle Frame)
{
    public Vector2 Size => new(Frame.Width, Frame.Height);
    
    public int Width => Frame.Width;
    
    public int Height => Frame.Height;
}
