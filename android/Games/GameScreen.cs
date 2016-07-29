using FallingCatGame.Scrollers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace FallingCatGame.Games
{
    public class GameScreen : GameBase, GameLogic
    {
        private BuildingScroller buildingScroller;
        private CloudScroller cloudScroller;

        public GameScreen(GameMain game) : base(game)
        {
            loadAssets();
        }

        private void loadAssets()
        {
            buildingScroller = new BuildingScroller(Content);
            cloudScroller = new CloudScroller(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            cloudScroller.Draw(spriteBatch);
            buildingScroller.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            cloudScroller.Update(gameTime);
            buildingScroller.Update(gameTime);
        }
    }
}