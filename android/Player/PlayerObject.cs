using FallingCatGame.Drawing;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Player
{
    public class PlayerObject : GameObject, IGameLogic
    {
        public PlayerObject(Texture2D texture, float scale)
            : base(texture, true, scale)
        {
            // Place the player in the center of the screen.
            float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Position = new Vector2(screenWidth / 2, screenHeight / 2);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}