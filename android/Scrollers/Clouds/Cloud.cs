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

        public abstract void LoadTexture();

        public Cloud(ContentManager content)
        {
            this.content = content;
            LoadTexture();
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update(int speed)
        {
            position.X += speed;
        }
    }
}