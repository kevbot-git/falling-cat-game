using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Player
{
    /// <summary>
    /// This object handles all operations on the score.
    /// GameScreen objects PlayerObject and BuildingScroller should hold references to this object.
    /// This object will also be used for saving the score to the device and contains methods to do so.
    /// The reason this object is seperate to other game objects is because this allows the score
    /// to be manipulated by different objects. For example, if a player gets a powerup that doubles
    /// their score for a short period of time, or defeating a certain enemy boosts the score.
    /// </summary>
    public class ScoreObject
    {
        private int _score;
        private float _scale;
        private Vector2 _position;
        private SpriteFont _font;

        public ScoreObject(ContentManager content, float scale)
        {
            _font = content.Load<SpriteFont>("fipps");
            _scale = scale;

            Texture2D buildingTexture = content.Load<Texture2D>("Building");
            _position = new Vector2(buildingTexture.Width * (scale / 2) + 10, 0);
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Score: " + _score, _position, Color.Black, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}