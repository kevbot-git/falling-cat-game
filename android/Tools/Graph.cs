using System.Collections.Generic;

namespace FallingCatGame.Tools
{
    /// <summary>
    /// Unfinished.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Graph<T> 
    {
        private HashSet<Vertex> _vertices;
        private HashSet<Edge> _edges;
        private Dictionary<Vertex, HashSet<Edge>> _adjacency;

        public Graph()
        {
            _vertices = new HashSet<Vertex>();
            _edges = new HashSet<Edge>();
            _adjacency = new Dictionary<Vertex, HashSet<Edge>>();
        }

        public HashSet<Vertex> Vertices
        {
            get { return _vertices; }
        }

        public HashSet<Edge> Edges
        {
            get { return _edges; }
        }

        /// <summary>
        /// Adds and returns a new isolated Vertex to the graph.
        /// </summary>
        /// <param name="t">The element to be passed into the Vertex</param>
        /// <returns></returns>
        public Vertex AddVertex(T t)
        {
            Vertex v = new Vertex(t);
            return AddVertex(v);
        }

        /// <summary>
        /// Helper method to add a Vertex object to the graph.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vertex AddVertex(Vertex v)
        {
            _vertices.Add(v);
            _adjacency.Add(v, new HashSet<Edge>());
            return v;
        }

        public Edge AddEdge(Vertex v0, Vertex v1)
        {
            if (!ContainsVertex(v0))
                AddVertex(v0);
            if (!ContainsVertex(v1))
                AddVertex(v1);

            Edge e = new Edge(v0, v1);
            _edges.Add(e);

            HashSet<Edge> edges;
            if (_adjacency.TryGetValue(v0, out edges))
                edges.Add(e);
            if (_adjacency.TryGetValue(v1, out edges))
                edges.Add(e);

            return e;
        }

        public bool ContainsVertex(Vertex v)
        {
            return _vertices.Contains(v);
        }

        public bool IsEmpty()
        {
            return _vertices.Count == 0;
        }

        public void Clear()
        {
            _vertices.Clear();
            _edges.Clear();
            _adjacency.Clear();
        }

        public class Vertex
        {
            // The element contained in the Vertex.
            private T _t;

            public Vertex(T t)
            {
                _t = t;
            }
        }

        public class Edge
        {
            private Vertex _v0;
            private Vertex _v1;

            public Edge(Vertex v0, Vertex v1)
            {
                _v0 = v0;
                _v1 = v1;
            }
        }
    }
}