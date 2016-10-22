using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Drawing
{
    /// <summary>
    /// A class that handles the variables that are common with most 2D game objects.
    /// When requiring the width or height of a game object this classes get methods should be used
    /// over texture get methods for width and height to return the correct post scale values.
    /// This class should be extended if creating a game object with unique characteristics.
    /// </summary>
    public class GameObject
    {
        // Standard 2D game object variables.
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Vector2 _direction;
        protected float _velocity;

        // Scale properties.
        protected float _width;
        protected float _height;
        protected float _scale;

        public GameObject(Texture2D texture, float scale, Vector2 position, Vector2 direction, float velocity)
        {
            _texture = texture;
            _scale = scale;
            _velocity = velocity;
            _direction = direction;
            _position = position;

            // Assigning scaled dimension.
            _width = texture.Width * scale;
            _height = texture.Height * scale;
        }

        // Constructor without velocity and direction.
        public GameObject(Texture2D texture, float scale, Vector2 position) : this(texture, scale, Vector2.Zero, Vector2.Zero, 0)
        {
            _width = texture.Width * scale;
            _height = texture.Height * scale;
        }

        /// <summary>
        /// A simple implementation of collision detection.
        /// Used as a default if an advanced method of collision does not need to be used.
        /// Creates and returns this objects hit box to be compared outside this class with anothers.
        /// </summary>
        public virtual Rectangle HitBox
        {
            get
            {
                return new Rectangle(
                    (int)_position.X,
                    (int)_position.Y,
                    (int)_width,
                    (int)_height);
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public float Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public int Width
        {
            get { return (int)_width; }
        }

        public int Height
        {
            get { return (int)_height; }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}