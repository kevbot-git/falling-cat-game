using System;
using FallingCatGame.Background;
using FallingCatGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FallingCatGame.Test.Tests
{
    public class CloudScrollerTest : UpdatingTestObject
    {
        private CloudScroller _cloudScroller;
        private GameObject _cloudToTest;
        private bool _testCloud = false;
        private float _screenWidth;

        public CloudScrollerTest(ContentManager content)
        {
            SetUp(content);
        }

        public void SetUp(ContentManager content)
        {
            // Select the two dominant lane textures to scale by. Game elements will scale in proportion to the following.
            Texture2D building = content.Load<Texture2D>("Building");
            Texture2D lane = content.Load<Texture2D>("Cat");

            // Store the calculated scale factors.
            ScaleHelper scale = new ScaleHelper(building.Width, lane.Width);

            // Create cloud scroller passing in it's relative scale.
            _cloudScroller = new CloudScroller(content, scale.LaneScale);

            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }

        public override void RunPostUpdateTests()
        {
            _assert.Equal(_cloudToTest.Position.X, _screenWidth, "Cloud hasn't been given a new position after going off screen.");
            _assert.TestResults("CloudScrollTest");
            _isFinished = true;
        }

        public override void RunPreUpdateTests()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (List<GameObject> layer in _cloudScroller.Clouds)
            {
                foreach (GameObject cloud in layer)
                {
                    if (cloud.Position.X < 0 - cloud.Width)
                    {
                        _cloudToTest = cloud;
                        _testCloud = true;
                    }
                }
            }

            _cloudScroller.Update(gameTime);

            if (_testCloud)
            {
                RunPostUpdateTests();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}