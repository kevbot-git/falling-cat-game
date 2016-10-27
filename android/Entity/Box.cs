using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using FallingCatGame.Player;
using FallingCatGame.Drawing;

namespace FallingCatGame
{
	public class Box : GameObject
	{
		PlayerObject _cat;

		public Box(PlayerObject cat, Texture2D texture, bool centerAsOrigin, float scale, Vector2 position, Vector2 direction, float velocity, SpriteEffects spriteEffect)
			: base(texture, centerAsOrigin, scale, position, direction, velocity, spriteEffect)
		{
			_cat = cat;
		}

		public override void action()
		{
			_cat.Hit = true;
		}
	}
}
