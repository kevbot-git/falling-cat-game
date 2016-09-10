using FallingCatGame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
	public class MenuScreen : IGameLogic
	{
		public MainMenu mainMenu;

		public MenuScreen(ContentManager content)
		{
			mainMenu = new MainMenu(content);
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