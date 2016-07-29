using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Scrollers.Buildings
{
    public class BuildingBrick : Building
    {
        public BuildingBrick(ContentManager content) : base(content)
        {

        }

        public override void LoadTexture()
        {
            texture = content.Load<Texture2D>("Building_Brick");
        }
    }
}