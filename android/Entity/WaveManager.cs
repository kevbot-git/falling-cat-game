using System.Collections.Generic;
using System;

using FallingCatGame.Main;
using FallingCatGame.Player;
using FallingCatGame.Drawing;
using FallingCatGame.Background;
using FallingCatGame.Tools;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame
{
    public class WaveManager : IGameLogic
    {
        // Changes (seba-git) 28/10/16:
        //
        // Switched to Markov chain to control obstacle spawn frequency
        // and probability. Changed waves data structure to a Queue.
        // Updated all index iteration to foreach iteration.
        // Queues also maintain FIFO ordering, the first wave to be added
        // to the queue will always be the first to be removed. Therefore
        // use of Enqueue() and Dequeue() is best for the waves. The data
        // structure will only grow as large as the max number of waves
        // that were on the screen at one time. Code has been simplified
        // using Peek(), a convenient way to check if the first wave is off-screen,
        // instead of iterating through every to check if it's off screen whilst updating.
        //
        // Prevented memory leak by having only one instance of each
        // texture with the obstacles referencing them instead of
        // creating a new texture for each and holding each in memory.
        // Removed unnecessary storing of screen height, can be retrieved anywhere.
        // Created ShallowCopy() method for GameObject to forcefully return a copy
        // instead of a reference to it in memory.

        // The Queue that will hold the waves, FIFO.
        private Queue<Wave> waves;

        const float TIMER = 0.8f;
        float timer = TIMER;

        private PlayerObject _cat;

        private float _left;
        private float _center;
        private float _right;

        private ScoreObject _score;

        // Texture reference. Helps memory leaks, all obstacles reference a single instance of their texture
        // Instead of storing their own.
        Texture2D _coinTexture;
        Texture2D _boxTexture;

        // The Markov chain that controls spawn frequency.
        MarkovChain<GameObject> _obstacleChain;

        public WaveManager(ContentManager content, PlayerObject cat, PlayerControl control, ScoreObject score)
        {
            // Cat and score passed by reference to obstacles.
            _cat = cat;
            _score = score;

            // Get lane positions.
            _left = control._left;
            _center = control._center;
            _right = control._right;

            // Loads and stores only 2 textures in memory.
            _boxTexture = content.Load<Texture2D>("box");
            _coinTexture = content.Load<Texture2D>("coin2");

            // Create the obstacles objects to be used in Markov.
            GameObject coin = new Coin(_coinTexture, false, 1, new Vector2(), new Vector2(0, 1),
                                  BuildingScroller.OBJECT_VELOCITY, SpriteEffects.None, _score);
            GameObject box = new Box(_cat, _boxTexture, false, 1, new Vector2(), new Vector2(0, 1),
                                BuildingScroller.OBJECT_VELOCITY, SpriteEffects.None);

            // Create Markov chain. Self-loops and nodes are automatically created.
            _obstacleChain = new MarkovChain<GameObject>();
            // 90% chance of getting a box after a coin.
            _obstacleChain.AddEdge(coin, box, 0.9);
            // 40% chance of getting a coin after a box.
            _obstacleChain.AddEdge(box, coin, 0.4);

            // Instantiate the wave list with its first wave.
            waves = new Queue<Wave>();
            waves.Enqueue(new Wave(cat, createWave()));
        }

        public List<GameObject> createWave()
        {
            List<GameObject> wave = new List<GameObject>();

            Random r = new Random();
            List<float> positions = new List<float>
            {
                _left,
                _center,
                _right,
            };

            for (int i = 0; i < r.Next(1, 3); i++)
            {
                // Randomize position.
                // TODO(seba-git): Markov chain of all position possibilities to avoid overwriting indices.
                // For loop can also be eliminated by using a Markov chain in this instance.
                int c = r.Next(positions.Count);
                float lane = positions[c];

                // Get next obstacle from Markov chain.
                GameObject obstacle = _obstacleChain.NextState().ShallowCopy();
                obstacle.Position = new Vector2(lane - 32,
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
                wave.Add(obstacle);

                // Create path.
                positions.RemoveAt(c);
            }

            return wave;
        }

        public void Update(GameTime gameTime)
        {
            // Handle timer and spawn a wave every n seconds.
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                waves.Enqueue(new Wave(_cat, createWave()));
                timer = TIMER;
            }

            // Update all waves.
            foreach (Wave w in waves)
            {
                w.Update(gameTime);
            }

            // Empty check.
            if (waves.Count != 0)
            {
                // Check if the current first wave is off-screen.
                GameObject slowest = waves.Peek().Slowest;
                if (!(slowest.Position.Y >= -slowest.Height))
                {
                    // FIFO order, first wave added is always the first to be off-screen.
                    waves.Dequeue();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Wave w in waves)
            {
                w.Draw(spriteBatch);
            }
        }
    }
}