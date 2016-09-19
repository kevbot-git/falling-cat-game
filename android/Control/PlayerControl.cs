using System;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FallingCatGame.Drawing;
using Android.Util;

namespace FallingCatGame.Control
{
    class PlayerControl
    {
        private static readonly float ACCEL_THRESH = 2.0f;
        private static readonly float ANIM_TIME = 1f;

        public Sprite playerSprite { get; private set; }

        private readonly float centerX;
        private readonly float leftX;
        private readonly float rightX;
        private Lane lane;
        private Lane prevLane;
        private float lastChangeTime;
        private float lastChangePos;
        private bool isMoving;

        public PlayerControl(Sprite playerSprite, float centerX, float offset)
        {
            this.playerSprite = playerSprite;
            this.centerX = centerX;
            leftX = centerX - offset;
            rightX = centerX + offset;
            lane = Lane.CENTER;
            prevLane = Lane.CENTER;
            isMoving = false;
        }

        public void Update(GameTime gameTime, Vector3 accelerometer)
        {
            float cT = (float) gameTime.TotalGameTime.TotalSeconds;
            prevLane = lane;

            if (accelerometer.X < -ACCEL_THRESH)
            {
                // Move right
                lane = Lane.RIGHT;
            }
            else if (accelerometer.X > ACCEL_THRESH)
            {
                // Move left
                lane = Lane.LEFT;
            }
            else
            {
                // Re center
                lane = Lane.CENTER;
            }

            if (lane != prevLane)
            {
                lastChangeTime = cT;
                lastChangePos = playerSprite.Position.X;
                Log.Info("ABC123", "lastChangePos: " + lastChangePos);
            }

            switch(lane)
            {
                case Lane.LEFT:
                    playerSprite.Position = new Vector2(ease(lastChangeTime, cT, ANIM_TIME, centerX, playerSprite.Position.X, leftX), playerSprite.Position.Y);
                    break;

                case Lane.RIGHT:
                    playerSprite.Position = new Vector2(ease(lastChangeTime, cT, ANIM_TIME, centerX, playerSprite.Position.X, rightX), playerSprite.Position.Y);
                    break;

                case Lane.CENTER:
                    //playerSprite.Position = new Vector2(centerX, playerSprite.Position.Y);
                    
                    playerSprite.Position = new Vector2(ease(lastChangeTime, cT, ANIM_TIME, (lastChangePos > centerX) ? rightX : leftX, playerSprite.Position.X, centerX), playerSprite.Position.Y);
                    break;
            }
        }

        private float ease(float startTime, float currentTime, float animTime, float startX, float currentX, float targetX)
        {
            float elapsedTime = currentTime - startTime;

            if (elapsedTime >= animTime)
            {
                isMoving = false;
                return targetX;
            }
            else
            {
                isMoving = true;
                return (float)(startX - (((targetX - startX) / 2) * Math.Cos(elapsedTime * Math.PI) / animTime) + (targetX - startX) / 2);
            }
        }

        private enum Lane
        {
            LEFT = -1, CENTER = 0, RIGHT = 1

            //static float Position { get; set; }
        }
    }
}