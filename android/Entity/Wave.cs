using System.Collections.Generic;

using FallingCatGame.Main;
using FallingCatGame.Player;
using FallingCatGame.Drawing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame
{
	public class Wave : IGameLogic
	{
		private List<GameObject> _entities;

		// Each wave is passed the main player object to check for collisions.
		private PlayerObject _cat;

		// Hold a reference to the slowest object to tell when a wave has 
		// travelled beyond the bounds of the game camera.
		private GameObject slowest;

		public Wave(PlayerObject cat, List<GameObject> entities)
		{
			_cat = cat;
			_entities = entities;
			slowest = entities[0];
		}

		public void Update(GameTime gameTime)
		{
			foreach (GameObject entity in _entities)
			{
				if (entity.Position.Y > slowest.Position.Y)
					slowest = entity;

				if (entity.HitBox.Intersects(_cat.HitBox))
					entity.action();

				entity.Position -= new Vector2(0, 1) * entity.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (GameObject entity in _entities)
			{
				entity.Draw(spriteBatch);
			}
		}

		public bool Visible()
		{
			return (slowest.Position.Y >= -slowest.Height);
		}
	}
}
