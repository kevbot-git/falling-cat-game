using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FallingCatGame.Drawing
{
    public class AnimatedGameObject: GameObject
    {
        private static string DEFAULT_ANIM = "default";
        private static float DEFAULT_FPS = 4.0f;

        private Vector2 _origin;

        // AnimatedGameObjects can have multiple animation combinations from the same image
        private string _selectedClip;
        private Dictionary<string, AnimationClip> _clips;
        private List<Rectangle> _frames;
        private int _frameColumns;
        private int _frameRows;

        public AnimatedGameObject(Texture2D texture, bool centerAsOrigin, int rows, int columns, float scale, Vector2 position, Vector2 direction, float velocity, SpriteEffects spriteEffects)
            : base(texture, centerAsOrigin, scale, position, direction, velocity, spriteEffects)
        {
            // Divide w & h by the number of times a texture is split into frames
            _width /= (_frameColumns = columns);
            _height /= (_frameRows = rows);

            _frames = generateFrames();

            _clips = new Dictionary<string, AnimationClip>();
            _clips.Add(DEFAULT_ANIM, new AnimationClip(DEFAULT_FPS, 0));
            _selectedClip = DEFAULT_ANIM;
        }

        private List<Rectangle> generateFrames()
        {
            List<Rectangle> temp = new List<Rectangle>();
            int w = _texture.Width / _frameColumns;
            int h = _texture.Height / _frameRows;
            _origin = new Vector2(w / 2, h / 2);
            for (int y = 0; y < _frameRows; y++)
            {
                for (int x = 0; x < _frameColumns; x++)
                {
                    temp.Add(new Rectangle((w * x), (h * y), w, h));
                }
            }
            return temp;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (CurrentAnimation != null)
            {
                // Check if it's time for the next frame
                if (((int)gameTime.TotalGameTime.TotalMilliseconds) % (1000 / CurrentAnimation.Fps) == 0)
                {
                    CurrentAnimation.CycleIndex();
                }
            }
        }

        // Override Draw() to draw only current frame
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _frames[CurrentAnimation.CurrentIndex()], Color.White, 0f, _origin, _scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }

        public string AddAnimation(string name, AnimationClip animationClip)
        {
            // Check the name hasn't been used
            foreach (string s in _clips.Keys)
            {
                if (s.Equals(name))
                    return null;
            }

            // Check the clip's indices are valid
            foreach (int i in animationClip.Indices)
            {
                if (i < 0 || i >= _frames.Count)
                    return null;
            }

            // Add the valiated pair
            _clips.Add(name, animationClip);
            return name;
        }

        public void SetAnimation(string animationKey)
        {
            List<string> keys = new List<string>(_clips.Keys);
            foreach (string n in keys)
            {
                if (n.Equals(animationKey))
                    _selectedClip = n;
            }
        }

        public override Rectangle HitBox
        {
            get
            {
                return new Rectangle(
                    (int)_position.X,
                    (int)_position.Y,
                    _texture.Width / _frameColumns,
                    _texture.Height / _frameRows);
            }
        }

        public AnimationClip CurrentAnimation
        {
            get { return _clips[_selectedClip]; }
        }
    }
}