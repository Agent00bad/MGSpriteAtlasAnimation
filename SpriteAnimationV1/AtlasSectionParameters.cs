using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnimationV1;

public class AtlasSectionParameters
{
    /// <summary>
    /// optional frame id for identification and indexing
    /// </summary>
    public int? FrameId { get; set; }
    
    /// <summary>
    /// Rectangle section the frame is in
    /// </summary>
    public Rectangle Section { get; set; }
    public Vector2 Origin { get; set; }
}