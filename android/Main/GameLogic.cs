using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
    public interface GameLogic
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}