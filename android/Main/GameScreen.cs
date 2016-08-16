using FallingCatGame.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
    public class GameScreen : GameBase, IGameLogic
    {
        private BuildingScroller buildingScroller;
        private CloudScroller cloudScroller;

        public GameScreen(GameMain game) : base(game) { }

        public void LoadAssets()
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