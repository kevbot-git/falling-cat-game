using FallingCatGame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
	public class MenuScreen : GameBase, IGameLogic
	{
		public MainMenu mainMenu;
		private GameStates states;

		public MenuScreen(GameStates states) : base(game)
		{
			this.states = states;
			mainMenu = new MainMenu(Content, states);
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