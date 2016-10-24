using FallingCatGame.Background;
using FallingCatGame.Drawing;
using FallingCatGame.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
    public class GameScreen : IGameLogic
    {
        // Background objects.
        private BuildingScroller _buildingScroller;
        private CloudScroller _cloudScroller;

        // Player objects.
        private PlayerObject _player;
        internal PlayerControl _playerControl;
        private ScoreObject _score;

		// Object objects.. \_(ツ)_/¯
		private WaveManager _waveManager;

        public GameScreen(ContentManager content, GameStates state)
        {
            // Load the content by passing in the ContentManager and finding the dominant scale factors.
            LoadContent(content, Scale(content), state);
        }

        private ScaleHelper Scale(ContentManager content)
        {
            // Select the two dominant lane textures to scale by. Game elements will scale in proportion to the following.
            Texture2D building = content.Load<Texture2D>("Building");
            Texture2D lane = content.Load<Texture2D>("kitty");

            // Return the calculated scale factors.
            return new ScaleHelper(building.Width, lane.Width / 4);
        }

        /// <summary>
        /// Loads the content to be used in the game screen.
        /// All objects instantiated in this method should have their own respective load content methods.
        /// </summary>
        /// <param name="content">The ContentManager to be passed into an object to load its relevant textures.</param>
        /// <param name="scale">The ScaleHelper containing the calculated scale factors, to be selected and applied to the objects.</param>
        private void LoadContent(ContentManager content, ScaleHelper scale, GameStates state)
        {
            // Load the player.
            _player = new PlayerObject(content.Load<Texture2D>("kitty"), 1, 4, scale.LaneScale);
            _player.SetAnimation(_player.AddAnimation("falling", new AnimationClip(4.0f, 0, 1, 2, 3)));

            _playerControl = new PlayerControl(_player);
            _score = new ScoreObject(content, scale.LaneScale * 2);

            // Load the scrollers.
            _buildingScroller = new BuildingScroller(content, scale.BuildingScale, _score);
            _cloudScroller = new CloudScroller(content, scale.LaneScale);

			// Load the obstacles.
			_waveManager = new WaveManager(content, _player, _playerControl, state);
        }

        public void Update(GameTime gameTime)
        {
            // Update the scrollers.
            _cloudScroller.Update(gameTime);
            _buildingScroller.Update(gameTime);

            // Update the player.
            _player.Update(gameTime);
            _playerControl.Update(gameTime);

            // Update the obstacles.
			_waveManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the scrollers.
            _cloudScroller.Draw(spriteBatch);
            _buildingScroller.Draw(spriteBatch);

            // Draw the player.
            _player.Draw(spriteBatch);
            _score.Draw(spriteBatch);

            // Draw the obstacles.
			_waveManager.Draw(spriteBatch);
        }
    }
}