using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Test.Tests
{
    public abstract class UpdatingTestObject : IGameLogic
    {
        // The assert object to be used in UpdatingTestObject test classes.
        protected Assert _assert = new Assert();
        // The flag to let the Runner know if a test is finished for a test class.
        protected bool _isFinished = false;

        public bool IsFinished
        {
            get { return _isFinished; }
        }

        // Runs all tests that can be tested before an Update(). These could include spawn tests,
        // and checking if an object starts with the correct values.
        public abstract void RunPreUpdateTests();
        // Runs all tests after a condition has been passed after Update() has been called. This can
        // be used to check how an object has changed.
        public abstract void RunPostUpdateTests();
        // XNA Update() and Draw() methods.
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}