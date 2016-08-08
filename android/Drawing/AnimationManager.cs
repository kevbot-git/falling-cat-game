using System.Collections.Generic;

namespace FallingCatGame.Drawing
{
    public class AnimationManager
    {

        private List<AnimationSequence> animations;


        private class AnimationSequence
        {
            public static readonly double GLOBAL_ANIM_INTERVAL = 1 / 6;

            private List<int> sequence;
            private double interval;
            private bool isPlaying;

            public AnimationSequence()
            {

            }
        }
    }
}