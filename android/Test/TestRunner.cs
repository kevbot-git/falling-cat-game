using FallingCatGame.Main;
using FallingCatGame.Test.Tests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace FallingCatGame.Test
{
    public class TestRunner : IGameLogic
    {
        public Queue tests;
        public TestObject current;

        public TestRunner()
        {
            SetUp();
            RunTests();
        }

        private void SetUp()
        {
            tests.Enqueue(new ScrollTest());
            current = (TestObject)tests.Dequeue();
        }

        private void RunTests()
        {
            while(tests.Count != 0)
            {
                current.Run();
                current = (TestObject)tests.Dequeue();
            }
        }

        public void Update(GameTime gameTime)
        {
            if(current.IsRunning)
                current.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}