using System;
using System.Collections.Generic;
using FallingCatGame.Main;
using FallingCatGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Background
{
    public class BuildingScroller : IGameLogic
    {
        private int _screenHeight;
        private int _screenWidth;

        // Building properties.
        private Texture2D _buildingTexture;
        private float _scale;
        private float _velocity;

        // Scroller properties.
        private int _nBuildings;
        private Vector2 _leftLast;
        private Vector2 _leftFirst;
        private Vector2 _rightFirst;
        private Vector2 _rightLast;
        private LinkedList<GameObject> _leftBuildings;
        private LinkedList<GameObject> _rightBuildings;

        public BuildingScroller(ContentManager content, float scale)
        {
            // Drawing.
            LoadContent(content);
            _scale = scale;

            _leftBuildings = new LinkedList<GameObject>();
            _rightBuildings = new LinkedList<GameObject>();

            _velocity = 300;

            _screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            _nBuildings = (int)(_screenHeight / _buildingTexture.Height * scale);

            for (int i = 0; i < _nBuildings + 2; i++)
            {
                // No position passed into constructor as position is later initialized.
                GameObject building = new GameObject(_buildingTexture, _scale, Vector2.Zero, new Vector2(0, 1), _velocity);
                _leftBuildings.AddLast(building);
                _rightBuildings.AddLast(building);
            }

            InitiatePositions();
        }

        private void LoadContent(ContentManager content)
        {
            _buildingTexture = content.Load<Texture2D>("NYApartmentBuilding");
        }

        private void InitiatePositions()
        {
            int cY = _screenHeight;

            foreach (GameObject building in _leftBuildings)
            {
                building.Position = new Vector2(0, cY);
                cY -= building.Height;
            }

            foreach (GameObject building in _rightBuildings)
            {
                building.Position = new Vector2(_screenWidth - building.Width, cY);
                cY -= building.Height;
            }

            UpdatePositions();
        }

        private void UpdatePositions()
        {
            _leftFirst = _leftBuildings.First.Value.Position;
            _leftLast = _leftBuildings.Last.Value.Position;
            _rightFirst = _rightBuildings.First.Value.Position;
            _rightLast = _rightBuildings.Last.Value.Position;
        }

        /// <summary>
        /// Returns a building with a random texture.
        /// Used to randomize textures in the scroller.
        /// This may or may not look good in the final product as it is completely random.
        /// Markov chain can be applied here to give towers a logical look.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        private GameObject GetRandomBuilding(Random seed)
        {
            // Add possible textures to this array.
            Texture2D[] textures = new Texture2D[] { _buildingTexture };
            return new GameObject(textures[seed.Next(0, textures.Length)], _scale, Vector2.Zero, new Vector2(0, 1), _velocity);
        }

        public void Update(GameTime gameTime)
        {
            Random seed = new Random();

            if (_leftLast.Y < -_buildingTexture.Height * _scale)
            {
                GameObject stackBuilding = GetRandomBuilding(seed);
                stackBuilding.Position = new Vector2(0, _leftFirst.Y + stackBuilding.Height);
                _leftBuildings.AddFirst(stackBuilding);
                _leftBuildings.RemoveLast();
            }

            if (_rightLast.Y < -_buildingTexture.Height * _scale)
            {
                GameObject stackBuilding = GetRandomBuilding(seed);
                stackBuilding.Position = new Vector2(_screenWidth - stackBuilding.Width, _rightFirst.Y + stackBuilding.Height);
                _rightBuildings.AddFirst(stackBuilding);
                _rightBuildings.RemoveLast();
            }

            foreach (GameObject building in _leftBuildings)
            {
                building.Position -= new Vector2(0, 1) * building.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            foreach (GameObject building in _rightBuildings)
            {
                building.Position -= new Vector2(0, 1) * building.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            UpdatePositions();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject building in _leftBuildings)
            {
                building.Draw(spriteBatch);
            }

            foreach (GameObject building in _rightBuildings)
            {
                building.Draw(spriteBatch);
            }
        }
    }
}