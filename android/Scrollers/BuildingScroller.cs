using FallingCatGame.Games;
using FallingCatGame.Scrollers.Buildings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FallingCatGame.Scrollers
{
    public class BuildingScroller : GameLogic
    {
        private const int TEXTURE_HEIGHT = 384;
        private LinkedList<Building> buildings;
        private ContentManager content;
        private Vector2 last;
        private Vector2 first;
        private int screenHeight;
        private int screenWidth;
        private int nBuildings;
        private int speed;

        public BuildingScroller(ContentManager content)
        {
            this.content = content;

            buildings = new LinkedList<Building>();

            speed = 300;

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            nBuildings = screenHeight / TEXTURE_HEIGHT;

            for (int i = 0; i < nBuildings + 2; i++)
            {
                Building building = new BuildingBrick(content);
                buildings.AddLast(building);
            }

            initiatePositions();
        }

        private void initiatePositions()
        {
            int cY = screenHeight;

            foreach (Building building in buildings)
            {
                building.Position = new Vector2(0, cY);
                cY -= TEXTURE_HEIGHT;
            }

            updatePositions();
        }

        private void updatePositions()
        {
            first = buildings.First.Value.Position;
            last = buildings.Last.Value.Position;
        }

        private Building getRandomBuilding()
        {
            Building[] buildings = new Building[] {new BuildingBrick(content), new BuildingBillboard(content)};
            return buildings[new Random().Next(0, buildings.Length)];
        }

        public void Update(GameTime gameTime)
        {
            if (last.Y < -TEXTURE_HEIGHT)
            {
                Building stackBuilding = getRandomBuilding();
                stackBuilding.Position = new Vector2(0, first.Y + TEXTURE_HEIGHT);
                buildings.AddFirst(stackBuilding);
                buildings.RemoveLast();
            }

            foreach (Building building in buildings)
            {
                building.Update(gameTime, speed);
            }

            updatePositions();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Building building in buildings)
            {
                building.Draw(spriteBatch);
            }
        }
    }
}