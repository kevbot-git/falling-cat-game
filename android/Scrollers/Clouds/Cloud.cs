using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Scrollers.Clouds
{
    public abstract class Cloud
    {
        protected ContentManager content;
        protected Texture2D texture;
        protected Vector2 position;
        protected float scale;
        protected int speed;
        protected Color color;

        public abstract void LoadTexture();

        public Cloud(ContentManager content, float scale, int speed, Color color)
        {
            this.content = content;
            this.scale = scale;
            this.speed = speed;
            this.color = color;
            LoadTexture();
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            position.X -= speed;
        }
    }
}