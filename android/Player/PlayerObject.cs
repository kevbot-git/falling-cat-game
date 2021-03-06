using FallingCatGame.Drawing;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Player
{
    public class PlayerObject : AnimatedGameObject, IGameLogic
    {
        private bool _hit;

        public PlayerObject(Texture2D texture, int rows, int columns, float scale)
            : base(texture, true, rows, columns, scale, Vector2.Zero, Vector2.Zero, 0f, SpriteEffects.None)
        {
            // Place the player in the center of the screen.
            float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Position = new Vector2(screenWidth / 2, screenHeight / 2);

            _hit = false;
        }

        public bool Hit
        {
            get { return _hit; }
            set { _hit = value; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}