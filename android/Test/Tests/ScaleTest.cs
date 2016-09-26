using FallingCatGame.Drawing;

namespace FallingCatGame.Test.Tests
{
    public class ScaleTest : TestObject
    {
        public override void Run()
        {
            // Passing in different numbers to actual project.
            // This test will assert that any passed in texture sizes will scale proportionally, which allows us to try different looks.
            // This test will also assert that the scaling algorithm is consistent.
            // Expected results will be done by hand as this is mathematical.
            // Test is done with a screen width of 1080 and a lane number of 3.
            ScaleHelper scaleHelper = new ScaleHelper(64, 128);
            assert.Equal(2.10, scaleHelper.BuildingScale, "Scale factor for building should be close to expected.");
            assert.Equal(2.10, scaleHelper.LaneScale, "Scale factor for lane should be close to expected.");
            assert.TestResults("ScaleTest");
        }
    }
}