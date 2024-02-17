using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnimationV1;

public class AnimatedSprite
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
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
        this.Position = new Vector2(0, 0);
        this._rows = rows;
        this._columns = columns;
        this.Scale = scale;

        Sections = DivideSpriteAtlasIntoSections();
    }

    /// <param name="texture">The texture/sprite atlas for the animation</param>
    /// <param name="rows">The amount of rows the sprites take up</param>
    /// <param name="columns">The amount of Columns the sprites are take up</param>
    /// <param name="position"></param>
    /// <param name="scale">The scale of the pixels where 1 is default</param>
    public AnimatedSprite(Texture2D texture, Vector2 position, int columns = 1, int rows = 1, float scale = 1)
    {
        this.Texture = texture;
        this.Position = position;
        this._rows = rows;
        this._columns = columns;
        this.Scale = scale;

        Sections = DivideSpriteAtlasIntoSections();
    }

    /// <param name="texture">The texture/sprite atlas for the animation</param>
    /// <param name="rows">The amount of rows the sprites take up</param>
    /// <param name="columns">The amount of Columns the sprites are take up</param>
    /// <param name="position"></param>
    /// <param name="scale">The scale of the pixels where 1 is default</param>
    /// <param name="reverseIndexing">If set to true it will index the frames counting rows before columns</param>
    public AnimatedSprite(Texture2D texture, Vector2 position, bool reverseIndexing, int columns = 1, int rows = 1,
        float scale = 1)
    {
        this.Texture = texture;
        this.Position = position;
        this._rows = rows;
        this._columns = columns;
        this.Scale = scale;

        Sections = DivideSpriteAtlasIntoSections(reverseIndexing);
    }

    private List<AtlasSectionParameters> DivideSpriteAtlasIntoSections(bool reverse = false)
    {
        var returnValues = new List<AtlasSectionParameters>();
        //TODO: observe results and see if it the rounding done by int compromises the result or not, if it does make it double, float or decimal
        int spriteAreaWidth = Texture.Width / _columns;
        int spriteAreaHeight = Texture.Height / _rows;

        int rectanglePositionY;
        int rectanglePositionX;
        int loops = 0;

        if (reverse)
        {
            for (int columnIndex = _columns; columnIndex > 0; columnIndex--)
            {
                rectanglePositionX = Texture.Width - (spriteAreaWidth * columnIndex);
                for (int rowIndex = _rows; rowIndex > 0; rowIndex--)
                {
                    loops++;
                    rectanglePositionY = Texture.Height - (spriteAreaHeight * rowIndex);
                    returnValues.Add(new AtlasSectionParameters
                    {
                        FrameId = loops,
                        Row = _rows - rowIndex + 1,
                        Column = _columns - columnIndex + 1,
                        Section = new Rectangle(rectanglePositionX, rectanglePositionY, spriteAreaWidth,
                            spriteAreaHeight),
                        Origin = new Vector2(rectanglePositionX + ((float)spriteAreaWidth / 2),
                            rectanglePositionY + ((float)spriteAreaHeight / 2)),
                    });
                }
            }

            return returnValues;
        }

        for (int rowIndex = _rows; rowIndex > 0; rowIndex--)
        {
            rectanglePositionY = Texture.Height - (spriteAreaHeight * rowIndex);
            for (int columnIndex = _columns; columnIndex > 0; columnIndex--)
            {
                loops++;
                rectanglePositionX = Texture.Width - (spriteAreaWidth * columnIndex);
                returnValues.Add(new AtlasSectionParameters
                {
                    FrameId = loops,
                    Row = _rows - rowIndex + 1,
                    Column = _columns - columnIndex + 1,
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


    #region DrawMethods

    public void Draw(SpriteBatch spriteBatch, int frame)
    {
        if (frame > CountFrames()) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, Sections[frame - 1].Section, Color.White);
    }

    public void Draw(SpriteBatch spriteBatch, int frame, Color color)
    {
        if (frame > CountFrames()) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, Sections[frame - 1].Section, color);
    }
    
    public void Draw(SpriteBatch spriteBatch, int column, int row)
    {
        var section = Sections.FirstOrDefault(s => s.Column == column && s.Row == row);
        if (section == null) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, section.Section, Color.White);
    }
    public void Draw(SpriteBatch spriteBatch, int column, int row, Color color)
    {
        var section = Sections.FirstOrDefault(s => s.Column == column && s.Row == row);
        if (section == null) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, section.Section, color);
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position, int frame)
    {
        Position = position;
        if (frame > CountFrames()) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, Sections[frame - 1].Section, Color.White);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, int frame, Color color)
    {
        Position = position;
        if (frame > CountFrames()) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, Sections[frame - 1].Section, color);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, int column, int row)
    {
        Position = position;
        var section = Sections.FirstOrDefault(s => s.Column == column && s.Row == row);
        if (section == null) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, section.Section, Color.White);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, int column, int row, Color color)
    {
        Position = position;
        var section = Sections.FirstOrDefault(s => s.Column == column && s.Row == row);
        if (section == null) throw new Exception("frame is out of bounds");
        spriteBatch.Draw(Texture, Position, section.Section, color);
    }

    #endregion
}