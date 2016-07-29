using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Background
{
    public class Cloud
    {
        protected ContentManager content;
        protected Texture2D texture;
        protected Vector2 position;
        protected float scale;
        protected int speed;
        protected Color color;

        public Cloud(ContentManager content, float scale, int speed, Color color, Texture2D texture)
        {
            this.content = content;
            this.scale = scale;
            this.speed = speed;
            this.color = color;
            this.texture = texture;
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D Texture
        {
            set { texture = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}