using System;
using System.Collections.Generic;

namespace FallingCatGame.Tools
{
    /// <summary>
    /// An implementaion of a directed graph for use as a Markov Chain.
    /// A unique feature of this graph is that it autogenerates self loops on vertices if their sum traversal probability of outdegree edges is less than 1.
    /// Self loops are also automatically removed upon addition of an Edge that cancels out the self loop.
    /// This implementation satisfies the criteria of a Markov Chain that a vertices outdegree sum of traversal probabilities is always equal to one.
    /// This does not prevent a user from adding a self loop manually, however the user will not need to.
    /// As a Markov Chain this graph also includes functions for generating and returning states.
    /// </summary>
    /// <typeparam name="T">The object type held in this graph.</typeparam>
    public class MarkovChain<T>
    {
        private HashSet<Vertex> _vertices;
        private HashSet<Edge> _edges;
        private Dictionary<Vertex, HashSet<Edge>> _adjacency;

        // Used to decide the next path of traversal.
        private Random _random;
        // The current state in the Markov Chain.
        private Vertex _current;

        public MarkovChain()
        {
            _vertices = new HashSet<Vertex>();
            _edges = new HashSet<Edge>();
            _adjacency = new Dictionary<Vertex, HashSet<Edge>>();
            _random = new Random();
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
        /// Generates the next state of the Markov Chain.
        /// This algorithm works by generating a random double between 0 and 1,
        /// the edges of the current state Vertex are then iterated through,
        /// with their traversal probabilities added to a sum.
        /// At any iteration, if the random double is less than or equal to the sum,
        /// then the Edge being iterated is the Edge selected to be traversed.
        /// This works because the sum of all outgoing edges for a Vertex will always add to 1.
        /// </summary>
        /// <returns>The next state as T.</returns>
        public T NextState()
        {
            double r = _random.NextDouble();
            double sum = 0;
            // Get all connected traversal probabilities to the current Vertex.
            HashSet<Edge> adjacentEdges = _current.AdjacentEdges();
            foreach (Edge e in adjacentEdges) {
                // Add the traversal probability to the sum.
                sum += e.TraversalProbability;
                // If a random double between 0 and 1 is less than the sum of probabilities in this iteration.
                if (r <= sum)
                {
                    // This edge has been selected to be traversed.
                    _current = e.OppositeVertex(_current);
                    // Return the current state's object.
                    return _current.Object;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Adds a new isolated Vertex to the graph.
        /// </summary>
        /// <param name="t">The element to be passed into the Vertex.</param>
        /// <returns>The Vertex added to the graph.</returns>
        private Vertex AddVertex(T t)
        {
            Vertex v = new Vertex(t, this);
            return AddVertex(v);
        }

        /// <summary>
        /// Helper method to add a Vertex to the graph.
        /// The last added Vertex will be the current state in the Markov Chain.
        /// </summary>
        /// <param name="v">The Vertex added to the graph.</param>
        /// <returns></returns>
        private Vertex AddVertex(Vertex v)
        {
            _vertices.Add(v);
            _adjacency.Add(v, new HashSet<Edge>());
            _current = v;
            return v;
        }

        /// <summary>
        /// Helper method to pass raw types when adding a new Edge.
        /// </summary>
        /// <param name="t0">Source.</param>
        /// <param name="t1">Destination.</param>
        /// <param name="p">Traversal probability.</param>
        /// <returns></returns>
        public Edge AddEdge(T t0, T t1, double p)
        {
            Vertex v0 = new Vertex(t0, this);
            Vertex v1 = new Vertex(t1, this);

            // If the vertices do exist they are added to the graph.
            if (!ContainsVertex(v0))
                AddVertex(v0);
            if (!ContainsVertex(v1))
                AddVertex(v1);

            return AddEdge(v0, v1, p);
        }

        /// <summary>
        /// Adds a new directed Edge from source to destination Vertex with traversal probability.
        /// If the resulting source vertex outdegree sum of traversal probabilities is more than 1. The edge is not added.
        /// If the resulting source vertex outdegree sum of traversal probabilities is less than 1. A self loop edge is created to store the remainder.
        /// </summary>
        /// <param name="v0">The source vertex.</param>
        /// <param name="v1">The destination vertex.</param>
        /// <param name="p">The probability of traversal.</param>
        /// <returns>The added edge.</returns>
        public Edge AddEdge(Vertex v0, Vertex v1, double p)
        {
            Edge edge = new Edge(v0, v1, p);

            // Holds the returned reference to the source vertex connected edges.
            HashSet<Edge> edges;
            // Holds the source vertex outdegree sum of traversal probabilities.
            double sum;
            double remainder;
            // Retrieves the vertex from the adjacency list and returns a reference to its edges.
            if (_adjacency.TryGetValue(v0, out edges))
            {
                // Get the sum of edge traversal probabilities connected to the source vertex.
                // Ignore any self loops.
                sum = 0;
                foreach (Edge e in edges)
                    if (!e.IsSelfLoop())
                        sum += e.TraversalProbability;
                sum += p;

                // The new edge is added to the source vertex set of connected edges.
                if (sum == 1)
                {
                    if (!edge.IsSelfLoop())
                    {
                        edges.Add(edge);
                        // Traversal probabilities of out degree edges is equal to 1, remove self loop if exists.
                        RemoveSelfLoop(edges);
                    }
                    // If the edge is a self loop that sums the total traversal probability to 1, add or update the self loop.
                    else
                    {
                        // Remove existing self loop if present.
                        RemoveSelfLoop(edges);
                        // Add self loop.
                        edges.Add(edge);
                    }
                }
                // Sum of traversal probabilities is less than 1, add the edge and remainder as a self loop.
                else if (sum < 1)
                {
                    edges.Add(edge);
                    remainder = 1 - sum;
                    // Recursive call to add the remaining probability of traversal as a self loop.
                    AddEdge(v0, v0, remainder);
                }
                // Sum of traversal probabilities is over 1, do not add the edge.
                else
                    return null;

                // Finally add the edge as a reference to the set of edges in the graph.
                _edges.Add(edge);

                // Graph is directed so an edge is not added to the destination vertex.
            }

            return edge;
        }

        /// <summary>
        /// Helper method to remove a self loop on a Vertex.
        /// </summary>
        /// <param name="edges">A set of the vertices connected edges.</param>
        /// <returns>True if a self loop has been removed.</returns>
        private bool RemoveSelfLoop(HashSet<Edge> edges)
        {
            Edge toRemove = null;
            foreach (Edge e in edges)
                if (e.IsSelfLoop())
                {
                    toRemove = e;
                }
            if (toRemove != null)
            {
                return RemoveEdge(toRemove);
            }
            return false;
        }

        public bool RemoveEdge(Edge e)
        {
            if (!ContainsEdge(e))
                return false;
            else
            {
                // Remove edge from graph.
                _edges.Remove(e);

                // Remove edge from vertex adjacency list.
                HashSet<Edge> edges;
                Vertex[] vertices = e.EndVertices();
                if (_adjacency.TryGetValue(vertices[0], out edges))
                    edges.Remove(e);

                return true;
            }
        }

        public bool ContainsVertex(Vertex v)
        {
            return _vertices.Contains(v);
        }

        public bool ContainsEdge(Edge e)
        {
            return _edges.Contains(e);
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

        public override string ToString()
        {
            string output = "";
            HashSet<Edge> edges;
            foreach (Vertex v in _vertices)
            {
                if (_adjacency.TryGetValue(v, out edges))
                {
                    output += v.ToString() + ": ";
                    foreach (Edge e in edges)
                        output += e + " ";
                    output += "\n";
                } 
            }
            return output;
        }

        /// <summary>
        /// Inner class that implements a Vertex for the MarkovChainGraph.
        /// Each Vertex must contain a reference to the outer class. The reason for this is in C# nested classes are like C++ nested classes
        /// and therefore do not behave in the unique way that Java inner classes do.
        /// </summary>
        public class Vertex
        {
            // The type contained in the vertex.
            private T _t;
            // Each Vertex must contain a reference to the outer class.
            private MarkovChain<T> _outerGraph;

            public Vertex(T t, MarkovChain<T> outerGraph)
            {
                _t = t;
                _outerGraph = outerGraph;
            }

            /// <summary>
            /// Accessor methods for retrieving and setting the type contained in the Vertex.
            /// </summary>
            public T Object
            {
                get { return _t; }
                set { _t = value; }
            }

            /// <summary>
            /// Return the edges connected to this Vertex as a set.
            /// </summary>
            /// <returns>This vertices incident edges.</returns>
            public HashSet<Edge> AdjacentEdges()
            {
                // In C# nested types can access private and protected members of the containing type.
                HashSet<Edge> edges;
                if (_outerGraph._adjacency.TryGetValue(this, out edges))
                    return edges;
                return null;
            }

            /// <summary>
            /// Returns the vertices that are adjacent to this Vertex as a set.
            /// </summary>
            /// <returns>This vertices connected vertices.</returns>
            public HashSet<Vertex> AdjacentVertices()
            {
                HashSet<Edge> edges;
                HashSet<Vertex> vertices = new HashSet<Vertex>();
                if (_outerGraph._adjacency.TryGetValue(this, out edges))
                    foreach (Edge e in edges)
                        vertices.Add(e.OppositeVertex(this));
                return vertices;
            }

            public override bool Equals(object o)
            {
                var v = o as Vertex;

                if (v == null)
                {
                    return false;
                }

                return Object.Equals(v.Object);
            }

            public override int GetHashCode()
            {
                return Object.GetHashCode();
            }

            public override string ToString()
            {
                return _t.ToString();
            }
        }

        /// <summary>
        /// Inner class that implements an Edge for the MarkovChainGraph.
        /// An Edge in this context also holds the probability of traversal as well as the two connecting vertices.
        /// </summary>
        public class Edge
        {
            private Vertex _v0;
            private Vertex _v1;
            // The probability of traversal.
            private double _p;

            public Edge(Vertex v0, Vertex v1, double p)
            {
                _v0 = v0;
                _v1 = v1;
                _p = p;
            }

            public double TraversalProbability
            {
                get { return _p; }
            }

            public Vertex[] EndVertices()
            {
                Vertex[] vertices = new Vertex[2];
                vertices[0] = _v0;
                vertices[1] = _v1;
                return vertices;
            }

            /// <summary>
            /// Returns the opposite Vertex to the Vertex in argument.
            /// If the Edge is a self loop, the Vertex passed in is returned.
            /// If no such Vertex exists in the Edge, null is returned.
            /// </summary>
            /// <param name="v0">The source Vertex.</param>
            /// <returns>The opposite Vertex.</returns>
            public Vertex OppositeVertex(Vertex v0)
            {
                if (IsSelfLoop())
                    return v0;
                if (_v0.Equals(v0))
                    return _v1;
                if (_v1.Equals(v0))
                    return _v0;
                else
                    return null;
            }

            public bool IsSelfLoop()
            {
                if (_v0.Equals(_v1))
                    return true;
                else
                    return false;
            }

            public override string ToString()
            {
                return _v0.ToString() + "->" + _v1.ToString() + "=" + _p;
            }
        }
    }
}