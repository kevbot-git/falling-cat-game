using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FallingCatGame.Drawing
{
    public class AnimationClip
    {
        private LinkedList<int> _indices;
        private LinkedListNode<int> _currentIndex;

        public AnimationClip(float fps, params int[] indices)
        {
            Fps = fps;
            _indices = new LinkedList<int>(indices);
            _currentIndex = _indices.First;
        }

        public AnimationClip Rewind()
        {
            _currentIndex = _indices.First;
            return this;
        }

        // Return the current frame index
        public int CurrentIndex()
        {
            return _currentIndex.Value;
        }

        // Select next frame node
        public void CycleIndex()
        {
            if (_currentIndex.Next != null)
                _currentIndex = _currentIndex.Next;
            else
                _currentIndex = _indices.First;
        }

        internal LinkedList<int> Indices
        {
            get { return _indices; }
        }

        public float Fps { get; set; }
    }
}