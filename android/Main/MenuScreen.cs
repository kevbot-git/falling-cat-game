using FallingCatGame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
	public class MenuScreen : GameBase, IGameLogic
	{
		public MainMenu mainMenu;

		public MenuScreen(GameMain game) : base(game)
		{
			mainMenu = new MainMenu(Content);
		}

		public void Update(GameTime gameTime)
		{
			mainMenu.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			mainMenu.Draw(spriteBatch);
		}
	}
}