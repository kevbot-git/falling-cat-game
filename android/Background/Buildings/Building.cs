using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Background.Buildings
{
    public abstract class Building
    {
        protected ContentManager content;
        protected Texture2D texture;
        protected Vector2 position;

        public abstract void LoadTexture();

        public Building(ContentManager content)
        {
            this.content = content;
            LoadTexture();
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects effect)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, effect, 0f);
        }

        public void Update(GameTime gameTime, int speed)
        {
            position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}