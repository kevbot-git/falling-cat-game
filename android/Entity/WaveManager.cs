using System.Collections.Generic;
using System;

using FallingCatGame.Main;
using FallingCatGame.Player;
using FallingCatGame.Drawing;
using FallingCatGame.Background;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame
{
	public class WaveManager : IGameLogic
	{
		private List<Wave> waves;

		const float TIMER = 0.8f;
		float timer = TIMER;

		private ContentManager _content;
		private PlayerObject _cat;

		private float _left;
		private float _center;
		private float _right;

		private float _screenHeight;

		private ScoreObject _score;

		public WaveManager(ContentManager content, PlayerObject cat, PlayerControl control, ScoreObject score)
		{
			_content = content;
			_cat = cat;

			_left = control._left;
			_center = control._center;
			_right = control._right;

			_screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

			_score = score;

			// Instantiate the wave list with its first wave.
			waves = new List<Wave>{
				new Wave(cat, createWave()),
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
				GameObject go = null;
				switch (r.Next(2))
				{
					// TODO: Separate different objects into their own classes - Helicopter.cs, Coin.cs etc.
					case 0:
						go = new Coin(_content.Load<Texture2D>("coin2"), false, 1, new Vector2(), new Vector2(0, 1),
						              BuildingScroller.OBJECT_VELOCITY, SpriteEffects.None, _score);
						break;
					case 1:
						go = new Box(_cat, _content.Load<Texture2D>("box"), false, 1, new Vector2(), new Vector2(0, 1),
						             BuildingScroller.OBJECT_VELOCITY, SpriteEffects.None);
						break;
				}

				int c = r.Next(positions.Count);
				float lane = positions[c];
				go.Position = new Vector2(lane - 32, _screenHeight);
				wave.Add(go);
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
				waves.Add(new Wave(_cat, createWave()));
				timer = TIMER;
			}

			// Update the waves if they are visible on screen, otherwise remove them.
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