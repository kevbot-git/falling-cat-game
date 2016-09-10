using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Menus
{
    public class MainMenu : IGameLogic
    {
        private Button single;
        private Button multi;
        private Button settings;
        private Button cat;
        private Button level;
        private Button highscores;

        private Button[] buttons;

        private ContentManager content;

        public MainMenu(ContentManager content)
        {
            this.content = content;

            loadTextures();
        }

        public void loadTextures()
        {
            single = new Button(content.Load<Texture2D>("Menu_Long_Button"), GameStates.Playing);
            multi = new Button(content.Load<Texture2D>("Menu_Long_Button"), GameStates.MainMenu);
            settings = new Button(content.Load<Texture2D>("Menu_Short_Button"), GameStates.MainMenu);
            cat = new Button(content.Load<Texture2D>("Menu_Short_Button"), GameStates.MainMenu);
            level = new Button(content.Load<Texture2D>("Menu_Short_Button"), GameStates.MainMenu);
            highscores = new Button(content.Load<Texture2D>("Menu_Short_Button"), GameStates.MainMenu);

            this.buttons = new Button[] { single, multi, settings, cat, level, highscores };
        }

        // CheckCollision checks whether a button has been clicked and if so it returns that button.
        // TODO(sno6): Clean (un-nest) this mess.
        public Button CheckCollision(TouchCollection touches)
        {
            foreach (var touch in touches)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    foreach (var button in this.buttons)
                    {
                        if (button.Box.Contains(touch.Position))
                        {
                            return button;
                        }
                    }
                }
            }

            return null;
        }

        public void Update(GameTime gametime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            // Vertical padding between menu buttons.
            int pad = width / 12;

            // Set the x, y, width and height of button elements in the main menu.
            single.Box = new Rectangle(width / 2 - single.Image.Width / 2, (height / 2) - single.Image.Height, single.Image.Width, single.Image.Height);
            multi.Box = new Rectangle(width / 2 - multi.Image.Width / 2, (height / 2) + pad, multi.Image.Width, multi.Image.Height);
            settings.Box = new Rectangle(single.Box.X, (height / 2) + multi.Image.Height + (pad * 2), settings.Image.Width, settings.Image.Height);
            cat.Box = new Rectangle((single.Box.X + single.Image.Width) - cat.Image.Width, (height / 2) + multi.Image.Height + (pad * 2), cat.Image.Width, cat.Image.Height);
            level.Box = new Rectangle(single.Box.X, (height / 2) + (multi.Image.Height * 2) + (pad * 3), level.Image.Width, level.Image.Height);
            highscores.Box = new Rectangle((single.Box.X + single.Image.Width) - highscores.Image.Width, (height / 2) + (multi.Image.Height * 2) + (pad * 3), highscores.Image.Width, highscores.Image.Height);

            foreach (var button in buttons)
            {
                spriteBatch.Draw(button.Image, destinationRectangle: button.Box);
            }
        }
    }
}