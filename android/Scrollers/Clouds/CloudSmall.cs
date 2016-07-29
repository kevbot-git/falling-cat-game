using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Scrollers.Clouds
{
    public class CloudSmall : Cloud
    {
        public CloudSmall(ContentManager content, float scale, int speed, Color color) : base(content, scale, speed, color)
        {
        }

        public override void LoadTexture()
        {
            texture = content.Load<Texture2D>("Cloud_Small");
        }
    }
}