using System;
using FallingCatGame.Main;
using FallingCatGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FallingCatGame.Background
{
    public class CloudScroller : IGameLogic
    {
        // The number of clouds on screen at a given time, this number is divided by the number of cloud layers.
        private const int NumberOfClouds = 6;

        private int _screenWidth;
        private int _screenHeight;

        // Cloud properties.
        private Texture2D _cloudTexture;
        private float _scale;

        // Scroller properties.
        private List<List<GameObject>> _clouds;
        private List<GameObject> _firstLayer;
        private List<GameObject> _secondLayer;
        private List<GameObject> _thirdLayer;

        public CloudScroller(ContentManager content, float scale)
        {
            _screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            _firstLayer = new List<GameObject>();
            _secondLayer = new List<GameObject>();
            _thirdLayer = new List<GameObject>();
            _clouds = new List<List<GameObject>>();

            _scale = scale;
            LoadContent(content);
            InitializeClouds();
        }

        private void LoadContent(ContentManager content)
        {
            _cloudTexture = content.Load<Texture2D>("Cloud");
        }

        private void InitializeClouds()
        {
			int nClouds = NumberOfClouds / 3;
            Random seed = new Random();

            for (int i = 0; i < nClouds; i++)
            {
                _firstLayer.Add(new GameObject(_cloudTexture, _scale, Vector2.Zero, new Vector2(1, 0), seed.Next(100, 200)));
                _secondLayer.Add(new GameObject(_cloudTexture, _scale * 2, Vector2.Zero, new Vector2(1, 0), seed.Next(50, 100)));
                _thirdLayer.Add(new GameObject(_cloudTexture, _scale * 3, Vector2.Zero, new Vector2(1, 0),  seed.Next(25, 50)));
            }

            _clouds.Add(_thirdLayer);
            _clouds.Add(_secondLayer);
            _clouds.Add(_firstLayer);
        }

        private Vector2 GetRandomYPosition(Random seed)
        {
            return new Vector2(_screenWidth, seed.Next(0, _screenHeight / 2));
        }

        public void Update(GameTime gameTime)
        {
            Random seed = new Random();

            foreach (List<GameObject> layer in _clouds)
            {
                foreach (GameObject cloud in layer)
                {
                    if (cloud.Position.X < 0 - cloud.Width)
                    {
                        cloud.Position = GetRandomYPosition(seed);
                    }

                    cloud.Position -= cloud.Direction * cloud.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<GameObject> layer in _clouds)
            {
                foreach (GameObject cloud in layer)
                {
                    cloud.Draw(spriteBatch);
                }
            }
        }
    }
}