using Microsoft.Xna.Framework.Content;

namespace FallingCatGame.Main
{
    public abstract class GameBase
    {
        private GameMain game;
        private ContentManager content;

        public GameBase(GameMain game)
        {
            this.game = game;
            content = game.Content;
        }

        public ContentManager Content
        {
            get { return content; }
        }
    }
}