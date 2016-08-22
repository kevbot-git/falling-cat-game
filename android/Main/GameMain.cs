using Android.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using FallingCatGame.Menus;

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

        GameStates state;
        MainMenu menu;

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
            // Set which gestures are allowed for android users.
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.Hold;

            // Set the inital game state to the main menu.
            state = GameStates.MainMenu;

            gameScreen = new GameScreen(this);
            menu = new MainMenu(Content);

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

            var touches = TouchPanel.GetState();

            switch (state)
            {
                case GameStates.MainMenu:
                    Button b = menu.CheckCollision(touches);
                    if (b != null)
                        this.state = b.NextState;

                    menu.Update(gameTime);
                    break;
                case GameStates.Playing:
                    gameScreen.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

			GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (state)
            {
                case GameStates.MainMenu:
                    menu.Draw(spriteBatch);
                    break;
                case GameStates.Playing:
                    gameScreen.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}