using System;
using System.Collections;
using FallingCatGame.Main;
using FallingCatGame.Test.Tests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FallingCatGame.Test
{
    public class UpdatingTestRunner : IGameLogic
    {
        private Queue _tests;
        private UpdatingTestObject _current;
        private bool _isComplete;

        public UpdatingTestRunner(ContentManager content)
        {
            SetUp(content);
        }

        public void SetUp(ContentManager content)
        {
            _isComplete = false;
            _tests = new Queue();

            _tests.Enqueue(new ScrollTest(content));

            _tests.Enqueue(null);
            _current = (UpdatingTestObject)_tests.Dequeue();
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        public void Update(GameTime gameTime)
        {
            while (_current != null)
            {
                if (!_current.IsFinished)
                    _current.Update(gameTime);
                else
                    _current = (UpdatingTestObject)_tests.Dequeue();
            }
            _isComplete = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}