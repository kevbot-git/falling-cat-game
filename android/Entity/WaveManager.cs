using System.Collections.Generic;
using System;

using FallingCatGame.Main;
using FallingCatGame.Player;
using FallingCatGame.Drawing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame
{
	public class WaveManager : IGameLogic
	{
		private List<Wave> waves;

		// Spawn a new wave every n seconds.
		float timer = 1;
		const float TIMER = 1;

		private ContentManager _content;
		private PlayerObject _cat;
		private PlayerControl _control;

		private float _left;
		private float _center;
		private float _right;

		private float _screenHeight;

		private GameStates _state;

		public WaveManager(ContentManager content, PlayerObject cat, PlayerControl control, GameStates state)
		{
			_content = content;
			_cat = cat;
			_control = control;

			_left = control._left;
			_center = control._center;
			_right = control._right;

			_screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

			_state = state;

			// Instantiate the wave list with its first wave.
			waves = new List<Wave>{
				new Wave(cat, createWave(), state),
			};
		}

		// Naive implementation of creating random waves.
		// When we have more time, fix this up and use the markov/graph implementation.
		// TODO(sno6): Scale the objects and place them at lane - scaled width instead of lane - 32.
		public List<GameObject> createWave()
		{
			List<GameObject> wave = new List<GameObject>();

			Random r = new Random();
			List<float> positions = new List<float>
			{
				_left,
				_center,
				_right,
			};

			for (int i = 0; i < r.Next(1, 3); i++)
			{
				Texture2D t = null;
				switch (r.Next(2))
				{
					// TODO: Separate different objects into their own calles - Helicopter.cs, Coin.cs etc.
					case 0:
						t = _content.Load<Texture2D>("coin2");
						break;
					case 1:
						t = _content.Load<Texture2D>("box");
						break;
				}

				int c = r.Next(positions.Count);
				float lane = positions[c];
				wave.Add(new GameObject(t, false, 1, new Vector2(lane - 32, _screenHeight), new Vector2(0, 1), 500, SpriteEffects.None));
				positions.RemoveAt(c);
			}

			return wave;
		}

		public void Update(GameTime gameTime)
		{
			// Handle timer and spawn a wave every n seconds.
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			timer -= elapsed;
			if (timer < 0)
			{
				waves.Add(new Wave(_cat, createWave(), _state));
				timer = TIMER;
			}

			// Update the waves if they are visible on screen.
			for (int i = 0; i < waves.Count; i++)
			{
				Wave w = waves[i];

				if (w.Visible())
				{
					w.Update(gameTime);
				}
				else
				{
					waves.RemoveAt(i);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Wave w in waves)
			{
				w.Draw(spriteBatch);
			}
		}
	}
}