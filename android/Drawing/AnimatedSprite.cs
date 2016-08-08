using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Android.Util;

namespace FallingCatGame.Drawing
{
    public class AnimatedSprite : Sprite
    {
        public List<Rectangle> FrameRects { get; protected set; }

        public AnimatedSprite(Texture2D texture, float scale, Vector2 position, float rotation, Point origin, SpriteBatch spriteBatch, int columns, int rows, int totalFrames, Rectangle colliderRect, double fps)
            : base(texture, scale, position, rotation, new Rectangle(), colliderRect, origin, spriteBatch)
        {
            FrameRects = new List<Rectangle>();
            int frameWidth = Texture.Width / columns;
            int frameHeight = Texture.Height / rows;
            int maxFrames = columns * rows;

            if(totalFrames < 1)
                totalFrames = 1;
            else if(totalFrames > maxFrames)
                totalFrames = maxFrames;

            int x, y = 0;

            for (int f = 0; f < totalFrames; f++) // 0
            {
                x = f % columns;
                
                FrameRects.Add(new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight));

                if (x == columns - 1)
                    y++;
            }

            DisplayRect = FrameRects[CurrentFrame];
            //TODO: Add unit testing to make sure frames are evenly divided
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, SpriteBatch spriteBatch, int columns, int rows, int totalFrames)
            : this(texture, GLOBAL_SCALE, position, 0f, Point.Zero, spriteBatch, columns, rows, totalFrames, texture.Bounds,GLOBAL_ANIM_RATE)
        {
            Origin = DisplayRect.Center;
            ColliderRect = DisplayRect;
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, SpriteBatch spriteBatch, int columns, int rows)
            : this(texture, position, spriteBatch, columns, rows, columns * rows) { }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds - lastAnimTick > animTick && AnimatingNow)
            {
                currentAnim.CurrentPosition++;
                DisplayRect = FrameRects[CurrentFrame];
                lastAnimTick = gameTime.TotalGameTime.TotalSeconds;
            }
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {            
            base.Render(gameTime);
        }

        public string CurrentAnim
        {
            get
            {
                return currentAnimKey;
            }
            set
            {
                if (Animations.ContainsKey(value))
                {
                    currentAnimKey = value;
                    currentAnim = Animations[currentAnimKey];
                    Animations[currentAnimKey].Reset();
                }
            }
        }

        public int CurrentFrame
        {
            get
            {
                return currentAnim.Frames[currentAnim.CurrentPosition];
            }
        }
    }
}