using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Background.Buildings
{
    public class BuildingBillboard : Building
    {
        public BuildingBillboard(ContentManager content) : base(content)
        {
        }

        public override void LoadTexture()
        {
            texture = content.Load<Texture2D>("Building_Brick_Billboard");
        }
    }
}