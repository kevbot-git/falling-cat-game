using System;
using FallingCatGame.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using FallingCatGame.Scrollers.Clouds;

namespace FallingCatGame.Scrollers
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

        public CloudScroller(ContentManager content)
        {
            this.content = content;

            firstLayer = new List<Cloud>();
            secondLayer = new List<Cloud>();
            thirdLayer = new List<Cloud>();
            clouds = new List<List<Cloud>>();

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            initializeClouds();
        }

        private void initializeClouds()
        {
            int nClouds = NUMBER_OF_CLOUDS / 3;
            Random seed = new Random();

            for (int i = 0; i < nClouds; i++)
            {
                firstLayer.Add(getRandomCloud(1f, seed.Next(3, 6), Color.White));
                firstLayer[i].Position = getRandomYPosition(seed);
                secondLayer.Add(getRandomCloud(0.67f, seed.Next(2, 4), Color.LightGray));
                secondLayer[i].Position = getRandomYPosition(seed);
                thirdLayer.Add(getRandomCloud(0.34f, seed.Next(1, 2), Color.SlateGray));
                thirdLayer[i].Position = getRandomYPosition(seed);
            }

            clouds.Add(thirdLayer);
            clouds.Add(secondLayer);
            clouds.Add(firstLayer);
        }

        private Vector2 getRandomYPosition(Random seed)
        {
            return new Vector2(screenWidth, seed.Next(0, screenHeight / 2));
        }

        private Cloud getRandomCloud(float scale, int speed, Color color)
        {
            Cloud[] clouds = new Cloud[] { new CloudSmall(content, scale, speed, color), new CloudMedium(content, scale, speed, color), new CloudLarge(content, scale, speed, color) };
            return clouds[new Random().Next(0, clouds.Length)];
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
                    }

                    cloud.Update();
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