using System.Collections.Generic;

namespace FallingCatGame.Sprite
{
    class AnimationSequence
    {
        public static readonly string DEFAULT = "default";

        public List<int> Frames { get; protected set; }

        public AnimationSequence() : this(0) { }

        public AnimationSequence(params int[] frames)
        {
            Frames = new List<int>();
            foreach(int i in frames)
            {
                Frames.Add(i);
            }
        }

        public class Map : Dictionary<string, AnimationSequence>
        {
            public Map(string name) : base()
            {
                Add(name, new AnimationSequence());
            }

            public Map() : this(DEFAULT) { }
        }
    }
}