using System;
using FallingCatGame.Drawing;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Android.Util;

namespace FallingCatGame.Player
{
    class PlayerObject : AnimatedGameObject, IGameLogic
    {
        public PlayerObject(Texture2D texture, int rows, int columns, float scale, Vector2 position)
            : base(texture, rows, columns, scale, position, Vector2.Zero, 0f) { }

        public override void Update(GameTime gameTime)
        {
            Log.Debug("CAT", "Frame: " + CurrentAnimation.CurrentIndex());
            base.Update(gameTime);
        }
    }
}