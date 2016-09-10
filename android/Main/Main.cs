using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using FallingCatGame.Menus;

namespace FallingCatGame.Main
{
    public class Main : Game
    {
        // XNA Standard.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameScreen gameScreen;

        GameStates state;
        MainMenu menu;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
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

            gameScreen = new GameScreen(Content);
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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var touches = TouchPanel.GetState();

            switch (state)
            {
                case GameStates.MainMenu:
                    Button b = menu.CheckCollision(touches);
                    if (b != null)
                        state = b.NextState;

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