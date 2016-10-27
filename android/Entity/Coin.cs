using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FallingCatGame.Drawing;
using FallingCatGame.Player;

namespace FallingCatGame
{
	public class Coin : GameObject
	{
		private ScoreObject _score;

		public Coin(Texture2D texture, bool centerAsOrigin, float scale, Vector2 position, Vector2 direction, float velocity, SpriteEffects spriteEffect, ScoreObject score)
			: base(texture, centerAsOrigin, scale, position, direction, velocity, spriteEffect) 
		{
			_score = score;
		}

		public override void action()
		{
			_score.Score += 10;

			// Make the coin ping away.
			base.Velocity = 3500;
		}
	}
}