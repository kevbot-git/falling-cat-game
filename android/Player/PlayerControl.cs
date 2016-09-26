using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace FallingCatGame.Player
{
    class PlayerControl
    {
        private static readonly float ACCEL_THRESH = 2.0f;
        private static readonly float ANIM_TIME = 1f;

        internal Vector3 accel;

        private PlayerObject _player;
        private float _left;
        private float _right;
        private float _center;

        public PlayerControl(PlayerObject player, float left, float right, float center)
        {
            _player = player;
            _left = left;
            _right = right;
            _center = center;
        }

        public void Update(GameTime gameTime)
        {
            if (accel != null)
            {
                float x;
                if (accel.X < -ACCEL_THRESH)
                {
                    // Move to right lane
                    x = _right;
                }
                else if (accel.X > ACCEL_THRESH)
                {
                    // Move to left lane
                    x = _left;
                }
                else
                {
                    // Move to center lane
                    x = _center;
                }

                _player.Position = new Vector2(x, _player.Position.Y);
            }
        }
    }
}