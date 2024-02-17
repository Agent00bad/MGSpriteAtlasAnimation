using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnimationV1;

public class AnimatedSprite
{
    public Texture2D Texture { get; set; }
    public Vector2 InitialPosition { get; set; }
    public float Scale { get; set; }

    private int
        _rows,
        _columns;

    public List<AtlasSectionParameters> Sections { get; set; }

    /// <param name="texture">The texture/sprite atlas for the animation</param>
    /// <param name="rows">The amount of rows the sprites take up</param>
    /// <param name="columns">The amount of Columns the sprites are take up</param>
    /// <param name="scale">The scale of the pixels where 1 is default</param>
    public AnimatedSprite(Texture2D texture, int columns = 1, int rows = 1, float scale = 1)
    {
        this.Texture = texture;
        this.InitialPosition = new Vector2(0, 0);
        this._rows = rows;
        this._columns = columns;
        this.Scale = scale;

        Sections = DivideSpriteAtlasIntoSections();
    }

    /// <param name="texture">The texture/sprite atlas for the animation</param>
    /// <param name="rows">The amount of rows the sprites take up</param>
    /// <param name="columns">The amount of Columns the sprites are take up</param>
    /// <param name="initialPosition"></param>
    /// <param name="scale">The scale of the pixels where 1 is default</param>
    public AnimatedSprite(Texture2D texture, Vector2 initialPosition, int columns = 1, int rows = 1, float scale = 1)
    {
        this.Texture = texture;
        this.InitialPosition = initialPosition;
        this._rows = rows;
        this._columns = columns;
        this.Scale = scale;

        Sections = DivideSpriteAtlasIntoSections();
    }

    private List<AtlasSectionParameters> DivideSpriteAtlasIntoSections()
    {
        var returnValues = new List<AtlasSectionParameters>();
        //TODO: observe results and see if it the rounding done by int compromises the result or not, if it does make it double, float or decimal
        int spriteAreaWidth = Texture.Width / _columns;
        int spriteAreaHeight = Texture.Height / _rows;

        int loops = 0;
        for (int rowIndex = _rows; rowIndex > 0; rowIndex--)
        {
            int rectanglePositionY = Texture.Height - (spriteAreaHeight * rowIndex);
            for (int columnIndex = _columns; columnIndex > 0; columnIndex--)
            {
                loops++;
                int rectanglePositionX = Texture.Width - (spriteAreaWidth * columnIndex);
                returnValues.Add(new AtlasSectionParameters
                {
                    FrameId = loops,
                    Section = new Rectangle(rectanglePositionX, rectanglePositionY, spriteAreaWidth, spriteAreaHeight),
                    Origin = new Vector2(rectanglePositionX + ((float)spriteAreaWidth / 2),
                        rectanglePositionY + ((float)spriteAreaHeight / 2)),
                });
            }
        }


        return returnValues;
    }

    /// <returns>The amount of frames from top left corner of atlas to bottom right</returns>
    public int CountFrames() => Sections.Count;

    public void Draw(SpriteBatch spriteBatch, Vector2 position, int frame)
    {
        if (frame > CountFrames()) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, position, Sections[frame - 1].Section, Color.White);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, int frame, Color color)
    {
        if (frame > CountFrames()) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, position, Sections[frame - 1].Section, color);
    }
}