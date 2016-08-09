using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Drawing
{
    public class Sprite
    {
        public static float GLOBAL_SCALE = 1f;

        public Texture2D Texture { get; protected set; }
        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Rectangle DisplayRect { get; protected set; }
        public Rectangle ColliderRect { get; protected set; }
        public Point Origin { get; protected set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
        public SpriteBatch Batch { get; protected set; }

        public Sprite(Texture2D texture, float scale, Vector2 position, float rotation, Rectangle displayRect, Rectangle colliderRect, Point origin, SpriteBatch spriteBatch)
        {
            Texture = texture;
            Scale = scale;
            Position = position;
            Rotation = rotation;
            DisplayRect = displayRect;
            ColliderRect = colliderRect;
            Origin = origin;
            Batch = spriteBatch;

            FlipHorizontal = false;
            FlipVertical = false;
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle displayRect, Rectangle colliderRect, SpriteBatch spriteBatch)
            : this(texture, GLOBAL_SCALE, position, 0f, displayRect, colliderRect, colliderRect.Center, spriteBatch) { }

        public Sprite(Texture2D texture, Vector2 position, SpriteBatch spriteBatch)
            : this(texture, position, texture.Bounds, texture.Bounds, spriteBatch) { }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Render(GameTime gameTime)
        {
            Batch.Draw(Texture, Position, DisplayRect, Color.White, Rotation, Origin.ToVector2(), Scale, Orientation, 0f);
        }

        public float Width { get { return DisplayRect.Width * Scale; } }
        public float Height { get { return DisplayRect.Height * Scale; } }

        public SpriteEffects Orientation
        {
            get
            {
                SpriteEffects temp = SpriteEffects.None;
                if (FlipHorizontal) temp |= SpriteEffects.FlipHorizontally;
                if (FlipVertical) temp |= SpriteEffects.FlipVertically;
                return temp;
            }
        }
    }
}