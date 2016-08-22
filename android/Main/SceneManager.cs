using Android.Util;
using FallingCatGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
    class SceneManager
    {
        public static int GRAPHICAL_LANE_WIDTH = 64; // Relative to drawing res as opposed to screen res
        public static int MINIMUM_BUILDING_SHOWING = 6; // At least this many display pixels will show // will be 64

        private GraphicsDevice graphicsDevice;

        public SceneManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            int minimumGraphicalWidth = GRAPHICAL_LANE_WIDTH * 3 + MINIMUM_BUILDING_SHOWING * 2;

            Scale = ActualBounds.Width / minimumGraphicalWidth; // Largest whole number that would fit on the screen
            Sprite.GLOBAL_SCALE = Scale;

            int xDifference = (ActualBounds.Width / Scale % minimumGraphicalWidth); // The remainder of the whole number (buildings)
            int targetWidth = minimumGraphicalWidth + xDifference;

            Log.Info("GAME_DEBUG", "Actual width: " + ActualBounds.Width
                + "px, min graphical width: " + minimumGraphicalWidth + "px, target width: " + targetWidth + "px, scale: " + Scale);

            Log.Info("GAME_DEBUG", (ActualBounds.Width % targetWidth == 0) ? "working!" : "remainder :/");

            TargetBounds = new Rectangle(xDifference / 2, 0, targetWidth, ActualBounds.Height / Scale);
        }

        public Rectangle ActualBounds { get { return graphicsDevice.Viewport.Bounds; } }

        public Rectangle TargetBounds { get; private set; }
        public int Scale { get; private set; }
    }
}