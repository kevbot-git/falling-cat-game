using FallingCatGame.Background;
using FallingCatGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FallingCatGame.Test.Tests
{
    /// <summary>
    /// This testing asserts that the buildings in the scroller spawn in the correct positions and are deleted when they go off screen.
    /// </summary>
    public class BuildingScrollerTest : UpdatingTestObject
    {
        private BuildingScroller _buildingScroller;
        // To test if the last building in the scroller is deleted at the right time.
        private GameObject _initLastBuilding;
        // Used to help test if the building is in the right position.
        private Texture2D _buildingTexture;
        private float _buildingScale;

        public BuildingScrollerTest(ContentManager content)
        {
            SetUp(content);
            RunPreUpdateTests();
        }

        public void SetUp(ContentManager content)
        {
            // Select the two dominant lane textures to scale by. Game elements will scale in proportion to the following.
            _buildingTexture = content.Load<Texture2D>("Building");
            Texture2D lane = content.Load<Texture2D>("Cat");

            // Store the calculated scale factors.
            ScaleHelper scale = new ScaleHelper(_buildingTexture.Width, lane.Width);
            _buildingScale = scale.BuildingScale;

            // Create building scroller passing in it's relative scale.
            _buildingScroller = new BuildingScroller(content, scale.BuildingScale);
        }

        public override void RunPreUpdateTests()
        {
            // Get first building and last building in scroller and their positions.
            float xFirstInitialPosition = _buildingScroller.LeftBuildings.First.Value.Position.X;
            float yFirstInitialPosition = _buildingScroller.LeftBuildings.First.Value.Position.Y;
            float xLastInitialPosition = _buildingScroller.LeftBuildings.Last.Value.Position.X;
            float yLastInitialPosition = _buildingScroller.LeftBuildings.Last.Value.Position.Y;

            // Get the screen height and height of building in scroller.
            float screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            float buildingHeight = _buildingScroller.LeftBuildings.First.Value.Height;

            // Test if the initial building spawn positions are correct.
            // Both first and last buildings in the scroller should have an X position of 0.
            _assert.Equal(xFirstInitialPosition, 0, "Bottom building on left did not spawn on the zero X axis.");
            _assert.Equal(xLastInitialPosition, 0, "Top building on left did not spawn on the zero X axis.");
            // Check if the first, or bottom building, spawned in the correct position.
            _assert.Equal(yFirstInitialPosition, screenHeight - buildingHeight, "Bottom building on left did not spawn on the correct Y axis.");
            // Check if the last, or top building, spawned in the correct position.
            _assert.Equal(yLastInitialPosition, 0 + buildingHeight, "Top building on left did not spawn on the correct Y axis.");

            // Store the last, or top, building object to test when if it was deleted when it went off screen.
            _initLastBuilding = _buildingScroller.LeftBuildings.Last.Value;
        }

        public override void RunPostUpdateTests()
        {
            // Assert that the last building had been deleted after the last update.
            _assert.NotEqual(_initLastBuilding, _buildingScroller.LeftBuildings.Last.Value, "Building in scroller was not deleted when it was off screen.");

            // Log test results and flag as finished.
            _assert.TestResults("BuildingScrollTest");
            _isFinished = true;
        }

        public override void Update(GameTime gameTime)
        {
            // Get the last buildings position before update.
            float _leftLastY = _buildingScroller.LeftBuildings.Last.Value.Position.Y;

            _buildingScroller.Update(gameTime);

            // Check if the last building should have been deleted on the last update.
            // If the Y position of the left top building is less than 0 - building height, it is offscreen.
            if (_leftLastY < -_buildingTexture.Height * _buildingScale)
            {
                // Building should have been deleted on the last update.
                RunPostUpdateTests();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}