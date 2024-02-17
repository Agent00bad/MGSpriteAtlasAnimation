using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteAnimationV1;

namespace TestSpriteAnimation;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private AnimatedSprite linkSprite;
    private AnimatedSprite catSprite;

    private int frameToUpdateAt = 30;
    private int currrentFrameSinceUpdate = 0;
    private int frameToShow = 1;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        linkSprite = new AnimatedSprite(Content.Load<Texture2D>("LinkTest"),4, 4);
        
        //from this wonderfull asset pack https://cupnooble.itch.io/sprout-lands-asset-pack
        catSprite = new AnimatedSprite(Content.Load<Texture2D>("CatSprite"), 2, 12);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        currrentFrameSinceUpdate++;
        if (frameToUpdateAt == currrentFrameSinceUpdate)
        {
            currrentFrameSinceUpdate = 0;
            if (frameToShow < linkSprite.CountFrames()) frameToShow++;
            else frameToShow = 1;
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here 
        
        _spriteBatch.Begin();
        linkSprite.Draw(_spriteBatch,new Vector2(100,100),4,4);
        catSprite.Draw(_spriteBatch,new Vector2(200,200),1);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}