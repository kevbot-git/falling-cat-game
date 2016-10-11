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
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _origin;
        private Vector2 _direction;
        private float _velocity;

        // Scale properties.
        private float _width;
        private float _height;
        private float _scale;

        private SpriteEffects _spriteEffect;

        public GameObject(Texture2D texture, bool centerAsOrigin, float scale, Vector2 position, Vector2 direction, float velocity, SpriteEffects spriteEffect)
        {
            _texture = texture;
            _scale = scale;
            _velocity = velocity;
            _direction = direction;
            _position = position;

            if (centerAsOrigin)
                _origin = new Vector2(texture.Width / 2, texture.Height / 2);
            else
                _origin = Vector2.Zero;

            // Assigning scaled dimension.
            _width = texture.Width * scale;
            _height = texture.Height * scale;

            _spriteEffect = spriteEffect;
        }

        // Constructor without velocity and direction.
        public GameObject(Texture2D texture, bool centerAsOrigin, float scale, Vector2 position) : this(texture, centerAsOrigin, scale, position, Vector2.Zero, 0, SpriteEffects.None)
        {}
        // Constructor without velocity and direction with mirroring.
        public GameObject(Texture2D texture, bool centerAsOrigin, float scale, Vector2 position, SpriteEffects spriteEffect) : this(texture, centerAsOrigin, scale, position, Vector2.Zero, 0, spriteEffect)
        {}
        // Constructor without position, velocity and direction.
        public GameObject(Texture2D texture, bool centerAsOrigin, float scale) : this(texture, centerAsOrigin, scale, Vector2.Zero, Vector2.Zero, 0, SpriteEffects.None)
        {}

        /// <summary>
        /// A simple implementation of collision detection.
        /// Used as a default if an advanced method of collision does not need to be used.
        /// Creates and returns this objects hit box to be compared outside this class with anothers.
        /// </summary>
        public Rectangle HitBox
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

        public Texture2D Texture
        {
            set { _texture = value; }
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, _origin, _scale, _spriteEffect, 0f);
        }
    }
}