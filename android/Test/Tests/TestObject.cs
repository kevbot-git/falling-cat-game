using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Test.Tests
{
    public abstract class TestObject : IGameLogic
    {
        protected bool _isRunning = false;

        public TestObject()
        {
            SetUp();
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public abstract void SetUp();
        public abstract void Run();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}