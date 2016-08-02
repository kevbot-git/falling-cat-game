using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FallingCatGame.Drawing
{
    public class AnimatedSprite : Sprite
    {
        public static double GLOBAL_ANIM_RATE = 4;

        public List<Rectangle> FrameRects { get; protected set; }
        public AnimationSequence.Map Animations { get; protected set; }
        public bool AnimatingNow { get; set; }

        private AnimationSequence currentAnim;
        private string currentAnimKey;
        private int currentFrame;
        private double lastAnimTick;
        private double animTick;

        public AnimatedSprite(Texture2D texture, float scale, Vector2 position, float rotation, Point origin, SpriteBatch spriteBatch, int columns, int rows, Rectangle colliderRect, double fps)
            : base(texture, scale, position, rotation, new Rectangle(), colliderRect, origin, spriteBatch)
        {
            Animations = new AnimationSequence.Map();
            CurrentAnim = AnimationSequence.DEFAULT;
            AnimatingNow = true;
            lastAnimTick = 0.0;
            animTick = 1 / fps;

            FrameRects = new List<Rectangle>();
            int frameWidth = Texture.Width / columns;
            int frameHeight = Texture.Height / rows;
            for(int y = 0; y < rows; y++)
            {
                for(int x = 0; x < columns; x++)
                {
                    FrameRects.Add(new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight));
                }
            }

            CurrentFrame = 0;
            DisplayRect = FrameRects[CurrentFrame];
            //TODO: Add unit testing to make sure frames are evenly divided
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, SpriteBatch spriteBatch, int columns, int rows)
            : this(texture, GLOBAL_SCALE, position, 0f, Point.Zero, spriteBatch, columns, rows, texture.Bounds,GLOBAL_ANIM_RATE)
        {
            Origin = DisplayRect.Center;
            ColliderRect = DisplayRect;
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds - lastAnimTick > animTick && AnimatingNow)
            {
                DisplayRect = FrameRects[CurrentFrame++];
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
                }
            }
        }

        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = Math.Abs(value) % FrameRects.Count;
            }
        }
    }
}