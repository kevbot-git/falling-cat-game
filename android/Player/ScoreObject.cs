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
        public const string HighScore = "HighScore.txt";

        private int _score;
        private int _highScore;
        private float _scale;
        private Vector2 _scorePosition;
        private Vector2 _highScorePosition;
        private SpriteFont _font;

        public ScoreObject(ContentManager content, float scale)
        {
            _font = content.Load<SpriteFont>("fipps");
            _scale = scale;

            Texture2D buildingTexture = content.Load<Texture2D>("Building");
            // + 10 is for position padding.
            _highScorePosition = new Vector2((buildingTexture.Width * (scale / 2)) + 10, 0);
            // Get height of font.
            Vector2 fontSize = _font.MeasureString("S");
            // + 10 is for position padding.
            _scorePosition = new Vector2((buildingTexture.Width * (scale / 2)) + 10, fontSize.Y * 2 + 10);
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "High Score: " + _highScore, _highScorePosition, Color.Black, 0f, Vector2.Zero, _scale / 2, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, "Score: " + _score, _scorePosition, Color.Black, 0f, Vector2.Zero, _scale / 2, SpriteEffects.None, 0f);
        }
    }
}