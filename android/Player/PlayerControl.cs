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
using Android.Util;

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
        private float _x = 0;

        private Assert _assert;

        public PlayerControl(PlayerObject player, float left, float right, float center)
        {
            _player = player;
            _left = left;
            _right = right;
            _center = center;

            _assert = new Assert();
        }

        public void Update(GameTime gameTime)
        {
            if (accel != null)
            {
                float prev_x = _x;
                if (accel.X < -ACCEL_THRESH)
                {
                    // Move to right lane
                    _x = _right;
                }
                else if (accel.X > ACCEL_THRESH)
                {
                    // Move to left lane
                    _x = _left;
                }
                else
                {
                    // Move to center lane
                    _x = _center;
                }

                _player.Position = new Vector2(_x, _player.Position.Y);
                if (_x != prev_x)
                    Log.Debug("FALLINGCAT", "Accelerometer: " + accel.X.ToString("0.0") +
                        " | threshold: " + ACCEL_THRESH.ToString("0.0") +
                        " | expected player x: " + _x.ToString("0.0") +
                        " | actual x: " + _player.Position.X.ToString("0.0") + " | " +
                        ((_player.Position.X.ToString("0.0").Equals(_x.ToString("0.0"))) ? "PASS": "FAIL"));
            }
        }
    }
}