using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Scrollers.Clouds
{
    public class CloudSmall : Cloud
    {
        public CloudSmall(ContentManager content) : base(content)
        {
        }

        public override void LoadTexture()
        {
            texture = content.Load<Texture2D>("Cloud_Small");
        }
    }
}