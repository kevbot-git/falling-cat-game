using System;
using System.Collections;
using FallingCatGame.Main;
using FallingCatGame.Test.Tests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FallingCatGame.Test
{
    /// <summary>
    /// This class is instantiated in GameMain.cs Initialize() method.
    /// All tests contained in this runner are ran through Update(). Tests stop when they have been flagged as finished.
    /// </summary>
    public class UpdatingTestRunner : IGameLogic
    {
        private Queue _tests;
        private UpdatingTestObject _current;
        // If all tests are complete, stop updating and switch the game state to playing.
        private bool _isComplete;

        /// <summary>
        /// Any Monogame or Android dependencies can be passed through the constructor and then through SetUp() into your tests.
        /// For example, ContentManager.
        /// </summary>
        public UpdatingTestRunner(ContentManager content)
        {
            SetUp(content);
        }

        public void SetUp(ContentManager content)
        {
            _isComplete = false;
            _tests = new Queue();

            // Enqueue all of your tests that require XNA's Update() or Draw() here.
            _tests.Enqueue(new ScrollTest(content));

            // Leave this.
            _tests.Enqueue(null);
            _current = (UpdatingTestObject)_tests.Dequeue();
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        /// <summary>
        /// Will run through all the tests in the queue.
        /// If a test is flagged as finished it is dequed.
        /// Once the queue is empty, complete will be called to notify GameMain.cs to change game state.
        /// </summary>
        /// <param name="gameTime"></param>
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