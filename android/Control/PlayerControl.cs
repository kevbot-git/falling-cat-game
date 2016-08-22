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
        private static readonly float ANIM_TIME = 0.8f;

        public Sprite playerSprite { get; private set; }

        private float centerX;
        private float leftX;
        private float rightX;
        private Movement movement;
        private Movement prevMovement;
        GameTime lastChange;

        public PlayerControl(Sprite playerSprite, float centerX, float offset)
        {
            this.playerSprite = playerSprite;
            this.centerX = centerX;
            leftX = centerX - offset;
            rightX = centerX + offset;
            movement = Movement.NONE;
            prevMovement = Movement.NONE;
        }

        public void Update(GameTime gameTime, Vector3 accelerometer)
        {
            prevMovement = movement;

            if (accelerometer.X < -ACCEL_THRESH)
            {
                // Move right
                movement = Movement.RIGHT;
            }
            else if (accelerometer.X > ACCEL_THRESH)
            {
                // Move left
                movement = Movement.LEFT;
            }
            else
            {
                // Re center
                movement = Movement.NONE;
            }

            if (movement != prevMovement)
            {
                lastChange = gameTime;
            }

            switch(movement)
            {
                case Movement.LEFT:
                    playerSprite.Position.X = leftX;
                    break;
                case Movement.RIGHT:

                    break;
                case Movement.NONE:

                    break;
            }
        }

        private enum Movement
        {
            LEFT = -1, NONE = 0, RIGHT = 1
        }
    }
}