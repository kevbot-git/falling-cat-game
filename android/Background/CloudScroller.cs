using System;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FallingCatGame.Background
{
    public class CloudScroller : GameLogic
    {
        private const int NUMBER_OF_CLOUDS = 6;
        private ContentManager content;
        private List<List<Cloud>> clouds;
        private List<Cloud> firstLayer;
        private List<Cloud> secondLayer;
        private List<Cloud> thirdLayer;
        private int screenWidth;
        private int screenHeight;
        private Texture2D cloudSmall;
        private Texture2D cloudMedium;
        private Texture2D cloudLarge;

        public CloudScroller(ContentManager content)
        {
            this.content = content;

            firstLayer = new List<Cloud>();
            secondLayer = new List<Cloud>();
            thirdLayer = new List<Cloud>();
            clouds = new List<List<Cloud>>();

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            loadContent();
            initializeClouds();
        }

        private void loadContent()
        {
            cloudSmall = content.Load<Texture2D>("Cloud_Small");
            cloudMedium = content.Load<Texture2D>("Cloud_Medium");
            cloudLarge = content.Load<Texture2D>("Cloud_Large");
        }

        private void initializeClouds()
        {
            int nClouds = NUMBER_OF_CLOUDS / 3;
            Random seed = new Random();

            for (int i = 0; i < nClouds; i++)
            {
                firstLayer.Add(getRandomCloud(1f, seed.Next(100, 200), Color.White, seed));
                secondLayer.Add(getRandomCloud(0.67f, seed.Next(50, 100), Color.LightGray, seed));
                thirdLayer.Add(getRandomCloud(0.34f, seed.Next(25, 50), Color.SlateGray, seed));
            }

            clouds.Add(thirdLayer);
            clouds.Add(secondLayer);
            clouds.Add(firstLayer);
        }

        private Vector2 getRandomYPosition(Random seed)
        {
            return new Vector2(screenWidth, seed.Next(0, screenHeight / 2));
        }

        private Texture2D getRandomTexture(Random seed)
        {
            Texture2D[] textures = new Texture2D[] { cloudSmall, cloudMedium, cloudLarge };
            return textures[seed.Next(0, textures.Length)];
        }

        private Cloud getRandomCloud(float scale, int speed, Color color, Random seed)
        {
            Cloud cloud = new Cloud(content, scale, speed, color, getRandomTexture(seed));
            cloud.Position = getRandomYPosition(seed);
            return cloud;
        }

        public void Update(GameTime gameTime)
        {
            Random seed = new Random();

            foreach (List<Cloud> layer in clouds)
            {
                foreach (Cloud cloud in layer)
                {
                    if (cloud.Position.X < 0)
                    {
                        cloud.Position = getRandomYPosition(seed);
                        cloud.Texture = getRandomTexture(seed);
                    }

                    cloud.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<Cloud> layer in clouds)
            {
                foreach (Cloud cloud in layer)
                {
                    cloud.Draw(spriteBatch);
                }
            }
        }
    }
}