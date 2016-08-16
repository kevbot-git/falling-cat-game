using Android.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Main
{
    class SceneManager
    {
        public static int GRAPHICAL_LANE_WIDTH = 64; // Relative to drawing res as opposed to screen res
        public static int MINIMUM_BUILDING_SHOWING = 6; // At least this many display pixels will show

        private GraphicsDevice graphicsDevice;

        public SceneManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            int minimumGraphicalWidth = GRAPHICAL_LANE_WIDTH * 3 + MINIMUM_BUILDING_SHOWING * 2;

            Scale = ActualBounds.Width / minimumGraphicalWidth;

            int targetWidth = minimumGraphicalWidth + (ActualBounds.Width / Scale % minimumGraphicalWidth);

            //Log.Info("GAME_DUBUG", "Actual width: " + ActualBounds.Width
            //    + "px, min graphical width: " + minimumGraphicalWidth + "px, target width: " + targetWidth + "px, scale: " + Scale);

            //Log.Info("GAME_DUBUG", (ActualBounds.Width % targetWidth == 0) ? "working!" : "remainder :/");

            TargetBounds = new Rectangle(0, 0, targetWidth, ActualBounds.Height / Scale);
        }

        public Rectangle ActualBounds { get { return graphicsDevice.Viewport.Bounds; } }

        public Rectangle TargetBounds { get; private set; }
        public int Scale { get; private set; }
    }
}