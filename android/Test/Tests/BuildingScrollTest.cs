using Android.Util;
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
            // Get the screen height and height of building in scroller.
            float screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            float buildingHeight = _buildingScroller.LeftBuildings.First.Value.Height;

            // Testing that the correct number of buildings are created initially.
            // If incorrect, too many building objects is wasteful of memory and could also cause problems later on when needing the correct number of buildings.
            // Too little buildings would cause visible gaps in the scroller.
            // Get number of buildings initialized in scroller.
            int nBuildings = _buildingScroller.LeftBuildings.Count - 1;
            // Divide the screenheight by post scaled building height.
            // If result does not match the actual height of a building, the number of buildings were calulated incorrectly through scaling or from an error in calculating.
            _assert.Equal(nBuildings, (int) Math.Round(screenHeight / buildingHeight, MidpointRounding.AwayFromZero), "Incorrect number of buildings initialized.");

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