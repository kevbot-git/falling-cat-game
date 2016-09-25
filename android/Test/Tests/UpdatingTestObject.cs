using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Test.Tests
{
    public abstract class UpdatingTestObject : IGameLogic
    {
        protected bool _isFinished = false;

        public bool IsFinished
        {
            get { return _isFinished; }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}