using FallingCatGame.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FallingCatGame.Test.Tests
{
    public class ScrollTest : UpdatingTestObject
    {
        private BuildingScroller buildingScroller;

        public ScrollTest(ContentManager content)
        {
            SetUp(content);
        }

        public void SetUp(ContentManager content)
        {
            buildingScroller = new BuildingScroller(content, 1);
        }

        public override void Update(GameTime gameTime)
        {
            buildingScroller.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}