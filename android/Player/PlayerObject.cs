using System;
using FallingCatGame.Drawing;
using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Player
{
    class PlayerObject : GameObject, IGameLogic
    {
        public PlayerObject(Texture2D texture, float scale, Vector2 position)
            : base(texture, scale, position)
        {

        }

        public void Update(GameTime gameTime)
        {
            //
        }
    }
}