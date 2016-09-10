using Microsoft.Xna.Framework.Graphics;

namespace FallingCatGame.Drawing
{
    /// <summary>
    /// Fixed width, variable height scaling.
    /// This class contains helper methods to calculate the dominant scale factors.
    /// This implementation of scaling is the most robust and accurate form of 
    /// scaling this game can implement.
    /// </summary>
    public class ScaleHelper
    {
        // The number of playable lanes. Hard-coded as it is a design choice.
        private const int NumberOfLanes = 3;

        // Store the calculated 'percentage of' to calculate scale factor.
        private float BuildingPct;
        private float LanePct;

        // Store the calculated scale factor for retrieval.
        private float _buildingScale;
        private float _laneScale;

        /// <summary>
        /// This object is used as a helper. Upon instantiation, values are calulated from the
        /// parameters and are set. This object is of no further use once the scale factors have been retreived.
        /// </summary>
        /// <param name="buildingWidth"></param>
        /// <param name="laneWidth"></param>
        public ScaleHelper(int buildingWidth, int laneWidth)
        {
            CalculateWidthPct(buildingWidth, laneWidth);
            CalculateScaleFactor(buildingWidth, laneWidth);
        }

        public float BuildingScale
        {
            get { return _buildingScale; }
        }

        public float LaneScale
        {
            get { return _laneScale; }
        }

        /// <summary>
        /// Calculates the percentage of which a building or lane should take up in width.
        /// For example, given the parameters, a building might require taking up 25% of the width of a screen.
        /// </summary>
        /// <param name="buildingWidth"></param>
        /// <param name="laneWidth"></param>
        private void CalculateWidthPct(int buildingWidth, int laneWidth)
        {
            float totalWidth = (buildingWidth * 2) + (laneWidth * NumberOfLanes);

            BuildingPct = buildingWidth / totalWidth;
            LanePct = laneWidth / totalWidth;
        }

        /// <summary>
        /// Calculates the two dominant scale factors.
        /// Calculated scale factor is precise. However this may or may not be desirable in the final look,
        /// an example being that we might want to create space between the buildings and the lanes.
        /// With this implementation we can easily do so by rounding down one of the dominant scale factors.
        /// Padding will be automatically applied through fixed position placement.
        /// </summary>
        /// <param name="buildingWidth"></param>
        /// <param name="laneWidth"></param>
        private void CalculateScaleFactor(int buildingWidth, int laneWidth)
        {
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float scaledBuildingWidth = screenWidth * BuildingPct;
            float scaledLaneWidth = screenWidth * LanePct;

            // Our pixel graphics should always need to be scaled up
            // Scale factor is found by dividing the actual width by the texture width
            // The sprite is then multiplied by this scale factor to match its width percentage of the screen
            _buildingScale = scaledBuildingWidth / buildingWidth;
            _laneScale = scaledLaneWidth / laneWidth;
        }
    }
}