using FallingCatGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame
{
    public class Button
    {
        private Rectangle box;
        private Texture2D image;

        // The purpose of a button in the main menu is to change the game's state so that another
        // area of the game can be rendered. nextState holds the value of the game state which
        // we wish to move to once an action is performed on this button (pressed & released)
        private GameStates nextState;

        public Button(Texture2D image, Rectangle box, GameStates nextState)
        {
            this.image = image;
            this.box = box;
            this.nextState = nextState;
        }

        public Button(Texture2D image, GameStates nextState)
        {
            this.image = image;
            this.nextState = nextState;
        }

        public Rectangle Box
        {
            get { return this.box; }
            set { this.box = value; }
        }

        public Texture2D Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        public GameStates NextState
        {
            get { return this.nextState; }
            set { this.nextState = value; }
        }
    }
}

