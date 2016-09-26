using FallingCatGame.Menus;
using FallingCatGame.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace FallingCatGame.Test.Tests
{
	public class MenuTest : TestObject
	{
		private MainMenu menu;

		public MenuTest(ContentManager content)
		{
			menu = new MainMenu(content);
		}

		public override void Run()
		{
			testButtons();
			testStates();
			testCollisions();

			assert.TestResults("MainMenu");
		}

		public void testCollisions()
		{
			Button first = menu.buttons[0];
			first.Box = new Rectangle(0, 0, 100, 100);

			TouchCollection correct = new TouchCollection();
			correct.Add(new TouchLocation(id: 1, state: TouchLocationState.Released,
			                              position: new Vector2(0, 0)));

			Button b = menu.CheckCollision(correct);
			assert.Equal(first, b, "Single button with correct collision was not detected.");

			TouchCollection wrong = new TouchCollection();
			wrong.Add(new TouchLocation(id: 2, state: TouchLocationState.Invalid,
										position: new Vector2(999, 999)));

			Button b2 = menu.CheckCollision(wrong);
			assert.Null(b2, "Touch detected at a random location returned a valid button.");
		}

		public void testStates()
		{
			assert.Equal(menu.buttons[0].NextState, GameStates.Playing, "Single player button has invalid state");
			assert.Equal(menu.buttons[1].NextState, GameStates.MainMenu, "Multi player button has invalid state");
			assert.Equal(menu.buttons[2].NextState, GameStates.MainMenu, "Settings button has invalid state");
			assert.Equal(menu.buttons[3].NextState, GameStates.MainMenu, "Change character button has invalid state");
			assert.Equal(menu.buttons[4].NextState, GameStates.MainMenu, "Change level button has invalid state");
			assert.Equal(menu.buttons[5].NextState, GameStates.MainMenu, "View highscores button has invalid state");
		}

		public void testButtons()
		{
			assert.Equal(menu.buttons.Length, 6, "Menu was created without correct number of items.");

			assert.NotNull(menu.buttons[0].Image, "Single player image is null");
			assert.NotNull(menu.buttons[1].Image, "Multi player image is null");
			assert.NotNull(menu.buttons[2].Image, "Settings image is null");
			assert.NotNull(menu.buttons[3].Image, "Change character image is null");
			assert.NotNull(menu.buttons[4].Image, "Change level image is null");
			assert.NotNull(menu.buttons[5].Image, "View highscores image is null");
		}
	}
}
