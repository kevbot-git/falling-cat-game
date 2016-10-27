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

		public Wave(PlayerObject cat, List<GameObject> entities)
		{
			_cat = cat;
			_entities = entities;
		}

		public void Update(GameTime gameTime)
		{
			foreach (GameObject entity in _entities)
			{
				if (_cat.HitBox.Intersects(entity.HitBox))
				{
                    _cat.Hit = true;
					_entities.Remove(entity);
					break;
				}

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

		// TODO(sno6): Fix this so waves are removed when they go out of visibility.
		public bool Visible()
		{
			// return _entities[0].Position.Y >= -_cat.Height;
			return true;
		}
	}
}
