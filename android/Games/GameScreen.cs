using FallingCatGame.Scrollers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Games
{
    public class GameScreen : GameBase, GameLogic
    {
        private BuildingScroller buildingScroller;

        public GameScreen(GameMain game) : base(game)
        {
            loadAssets();
            setBackground();
        }

        private void loadAssets()
        {
            buildingScroller = new BuildingScroller(Content);
        }

        private void setBackground()
        {

        }

        private void drawBuildings(SpriteBatch spriteBatch)
        {
            buildingScroller.Draw(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            drawBuildings(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            buildingScroller.Update(gameTime);
        }
    }
}