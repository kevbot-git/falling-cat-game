using FallingCatGame.Main;
using FallingCatGame.Background.Buildings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FallingCatGame.Background
{
    public class BuildingScroller : IGameLogic
    {
        private const int TEXTURE_HEIGHT = 384;
        private LinkedList<Building> leftBuildings;
        private LinkedList<Building> rightBuildings;
        private ContentManager content;
        private Vector2 leftLast;
        private Vector2 leftFirst;
        private Vector2 rightFirst;
        private Vector2 rightLast;
        private int screenHeight;
        private int screenWidth;
        private int nBuildings;
        private int speed;

        public BuildingScroller(ContentManager content)
        {
            this.content = content;

            leftBuildings = new LinkedList<Building>();
            rightBuildings = new LinkedList<Building>();

            speed = 300;

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            nBuildings = screenHeight / TEXTURE_HEIGHT;

            for (int i = 0; i < nBuildings + 2; i++)
            {
                Building building = new BuildingBrick(content);
                leftBuildings.AddLast(building);
                rightBuildings.AddLast(building);
            }

            initiatePositions();
        }

        private void initiatePositions()
        {
            int cY = screenHeight;

            foreach (Building building in leftBuildings)
            {
                building.Position = new Vector2(0, cY);
                cY -= TEXTURE_HEIGHT;
            }

            foreach (Building building in rightBuildings)
            {
                building.Position = new Vector2(screenWidth - building.Texture.Width, cY);
                cY -= TEXTURE_HEIGHT;
            }

            updatePositions();
        }

        private void updatePositions()
        {
            leftFirst = leftBuildings.First.Value.Position;
            leftLast = leftBuildings.Last.Value.Position;
            rightFirst = rightBuildings.First.Value.Position;
            rightLast = rightBuildings.Last.Value.Position;
        }

        private Building getRandomBuilding(Random seed)
        {
            Building[] buildings = new Building[] {new BuildingBrick(content), new BuildingBillboard(content)};
            return buildings[seed.Next(0, buildings.Length)];
        }

        public void Update(GameTime gameTime)
        {
            Random seed = new Random();

            if (leftLast.Y < -TEXTURE_HEIGHT)
            {
                Building stackBuilding = getRandomBuilding(seed);
                stackBuilding.Position = new Vector2(0, leftFirst.Y + TEXTURE_HEIGHT);
                leftBuildings.AddFirst(stackBuilding);
                leftBuildings.RemoveLast();
            }

            if (rightLast.Y < -TEXTURE_HEIGHT)
            {
                Building stackBuilding = getRandomBuilding(seed);
                stackBuilding.Position = new Vector2(screenWidth - stackBuilding.Texture.Width, rightFirst.Y + TEXTURE_HEIGHT);
                rightBuildings.AddFirst(stackBuilding);
                rightBuildings.RemoveLast();
            }

            foreach (Building building in leftBuildings)
            {
                building.Update(gameTime, speed);
            }

            foreach (Building building in rightBuildings)
            {
                building.Update(gameTime, speed);
            }

            updatePositions();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Building building in leftBuildings)
            {
                building.Draw(spriteBatch, SpriteEffects.None);
            }

            foreach (Building building in rightBuildings)
            {
                building.Draw(spriteBatch, SpriteEffects.FlipHorizontally);
            }
        }
    }
}