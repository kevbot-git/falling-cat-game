using Android.Util;
using FallingCatGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FallingCatGame.Main
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        public static readonly string DEBUG_TAG = "CAT_GAME";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameScreen gameScreen;

        // Testing sprites
        private Sprite staticBuilding;
        private AnimatedSprite animDude;

        public Vector3 Accelerometer { get; internal set; }

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;

            Accelerometer = new Vector3();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            gameScreen = new GameScreen(this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Testing sprites
            //staticBuilding = new Sprite(Content.Load<Texture2D>("Building_Brick"), GraphicsDevice.Viewport.Bounds.Center.ToVector2(), spriteBatch);
            animDude = new AnimatedSprite(Content.Load<Texture2D>("dude_colour"), GraphicsDevice.Viewport.Bounds.Center.ToVector2(), spriteBatch, 2, 2);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                // Write accel reading to debug log [TEMPORARY]
                Log.Info(DEBUG_TAG, "Accel.X: " + Accelerometer.X + " Accel.Y: " + Accelerometer.Y + " Accel.Z: " + Accelerometer.Z);
                // usually Exit() is called here;
            }
            // TODO: Add your update logic here

            gameScreen.Update(gameTime);

            // Testing sprites
            animDude.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
			// Testing..

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, null);
            gameScreen.Draw(spriteBatch);
            // Testing sprites
            animDude.Render(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
