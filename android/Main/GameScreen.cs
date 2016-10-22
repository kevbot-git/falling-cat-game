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
        private PlayerObject _player;

        internal PlayerControl playerControl;

        public GameScreen(ContentManager content)
        {
            // Load the content by passing in the ContentManager and finding the dominant scale factors.
            LoadContent(content, Scale(content));
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
        private void LoadContent(ContentManager content, ScaleHelper scale)
        {
            float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            // Load the scrollers.
            _buildingScroller = new BuildingScroller(content, scale.BuildingScale);
            _cloudScroller = new CloudScroller(content, scale.LaneScale);

            Texture2D cat = content.Load<Texture2D>("kitty");
            _player = new PlayerObject(cat, 1, 4, scale.LaneScale, new Vector2(screenWidth / 2, 20));
            _player.SetAnimation(_player.AddAnimation("falling", new AnimationClip(4.0f, 0, 1, 2, 3)));

            playerControl = new PlayerControl(_player, screenWidth / 2 - _player.Width, screenWidth / 2 + _player.Width, screenWidth / 2);
        }

        public void Update(GameTime gameTime)
        {
            playerControl.Update(gameTime);

            // Update the scrollers.
            _cloudScroller.Update(gameTime);
            _buildingScroller.Update(gameTime);

            _player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the scrollers.
            _cloudScroller.Draw(spriteBatch);
            _buildingScroller.Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }
    }
}