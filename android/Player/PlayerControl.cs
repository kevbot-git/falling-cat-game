using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Player
{
    public class PlayerControl
    {
        private static readonly float ACCEL_THRESH = 3.0f;
        private static readonly float ANIM_TIME = 1f;

        internal Vector3 accel;

        private PlayerObject _player;
        public float _left;
        public float _right;
        public float _center;

        public PlayerControl(PlayerObject player)
        {
            _player = player;

            // Assign movement positions.
            float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _left = (screenWidth / 2) - player.Width;
            _right = (screenWidth / 2) + player.Width;
            _center = screenWidth / 2;
        }

        public void Update(GameTime gameTime)
        {
            if (accel != null)
            {
                float x;
                if (accel.X < -ACCEL_THRESH)
                {
                    // Move to right lane.
                    x = _right;
                }
                else if (accel.X > ACCEL_THRESH)
                {
                    // Move to left lane.
                    x = _left;
                }
                else
                {
                    // Move to center lane.
                    x = _center;
                }

                _player.Position = new Vector2(x, _player.Position.Y);
            }
        }
    }
}