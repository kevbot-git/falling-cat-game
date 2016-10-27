using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FallingCatGame.Player
{
    public class PlayerControl
    {
        private static readonly float ACCEL_THRESH = 2.75f;
        private static readonly double ANIM_TIME = 0.15;

        public float _left;
        public float _right;
        public float _center;

        internal Vector3 accel;

        private PlayerObject _player;

        private int _target = 0;
        private int _current = 0;
        private float _lane;
        private double _lastTime;

        public PlayerControl(PlayerObject player)
        {
            _player = player;

            _center = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            _left = _center - player.Width;
            _right = _center + player.Height;
            _lastTime = 0.0;
        }

        public void Update(GameTime gameTime)
        {
            if (accel != null)
            {
                _current = (int) Lane;
                if (accel.X < -ACCEL_THRESH)
                {
                    if (_current < 1 && gameTime.TotalGameTime.TotalSeconds - _lastTime > ANIM_TIME)
                    {
                        // Move to right lane.
                        _lastTime = gameTime.TotalGameTime.TotalSeconds;
                        _target++;
                    }
                }
                else if (accel.X > ACCEL_THRESH)
                {
                    if (_current > -1 && gameTime.TotalGameTime.TotalSeconds - _lastTime > ANIM_TIME)
                    {
                        // Move to left lane.
                        _lastTime = gameTime.TotalGameTime.TotalSeconds;
                        _target--;
                    }
                }
                else { }

                Lane = _target;

                _player.Position = new Vector2(getPos(), _player.Position.Y);
            }
        }

        private float getPos()
        {
            return _center + Lane * _player.Width;
        }

        private float Lane
        {
            get { return _lane; }
            set
            {
                if (value > 1f)
                    _lane = 1f;
                if (value < -1f)
                    _lane = -1f;
                _lane = value;
            }
        }
    }
}