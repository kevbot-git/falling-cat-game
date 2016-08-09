using System;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FallingCatGame.Drawing;

namespace FallingCatGame.Control
{
    class PlayerControl
    {
        private static readonly float ACCEL_THRESH = 2.0f;

        public Sprite playerSprite { get; set; }

        private Vector2 center;
        private Vector2 left;
        private Vector2 right;

        public PlayerControl(Sprite playerSprite, Vector2 center, float offset)
        {
            this.playerSprite = playerSprite;
            this.center = center;
            left = center;
            right = center;
            left.X -= offset;
            right.X += offset;
        }

        public void Update(Vector3 accelerometer)
        {
            if(accelerometer.X < -ACCEL_THRESH)
            {
                playerSprite.Position = right;
            }
            else if(accelerometer.X > ACCEL_THRESH)
            {
                playerSprite.Position = left;
            }
            else
            {
                playerSprite.Position = center;
            }
        }
    }
}