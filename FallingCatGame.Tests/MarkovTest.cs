using System;
using FallingCatGame.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FallingCatGame.Tests
{
    [TestClass]
    public class MarkovTest
    {
        [TestMethod]
        // Exception is expected. If thrown, the test will pass.
        [ExpectedException(typeof(ArgumentException))]
        public void TestFailSum()
        {
            MarkovChain<string> mc = new MarkovChain<string>();
            mc.AddEdge("milk", "eggs", 0.8);
            // The source sum of probability traversals can not be higher than 1.
            // 0.8 + 0.4 = 1.2 or 120%. This should throw an exception.
            mc.AddEdge("milk", "bread", 0.4);
        }

        /// <summary>
        /// Self-loops in the Markov chain are automatically generated if
        /// the sum of probability of traversals coming out of a Vertex do not add to one.
        /// In a sense, this feature prevents errors and corrects itself. 
        /// If a user does not want a self-loop for an object, they should
        /// enforce that all outgoing edges add to 1.
        /// </summary>
        [TestMethod]
        public void TestSelfLoop()
        {
            MarkovChain<string> mc = new MarkovChain<string>();

            // Vertex and Edge objects are not meant to be accessed outside of the MarkovChain.
            // Hence the use of 'MarkovChain<string>.Vertex'.
            MarkovChain<string>.Vertex milk = mc.AddVertex("milk");
            MarkovChain<string>.Vertex eggs = mc.AddVertex("eggs");

            // A self-loop should be auto generated.
            mc.AddEdge(milk, eggs, 0.8);

            HashSet<MarkovChain<string>.Edge> milkEdges = milk.AdjacentEdges();
            MarkovChain<string>.Edge selfLoop = null;

            foreach (MarkovChain<string>.Edge milkEdge in milkEdges)
            {
                if (milkEdge.IsSelfLoop())
                    selfLoop = milkEdge;
            }

            // Test that a self-loop exists.
            Assert.IsNotNull(selfLoop);
            // Test that the self-loop is correct.
            Assert.AreEqual(0.2, selfLoop.TraversalProbability, 0.01);
        }

        /// <summary>
        /// Tests if a self-loop automatically updates upon addition of a new Edge.
        /// </summary>
        [TestMethod]
        public void TestSelfLoopUpdate()
        {
            MarkovChain<string> mc = new MarkovChain<string>();

            // Vertex and Edge objects are not meant to be accessed outside of the MarkovChain.
            // Hence the use of 'MarkovChain<string>.Vertex'.
            MarkovChain<string>.Vertex milk = mc.AddVertex("milk");
            MarkovChain<string>.Vertex eggs = mc.AddVertex("eggs");

            // A self-loop should be auto generated.
            mc.AddEdge(milk, eggs, 0.8);
            // The self-loop should update.
            mc.AddEdge("milk", "beer", 0.1);

            HashSet<MarkovChain<string>.Edge> milkEdges = milk.AdjacentEdges();
            MarkovChain<string>.Edge selfLoop = null;

            // Find the self-loop.
            foreach (MarkovChain<string>.Edge milkEdge in milkEdges)
            {
                if (milkEdge.IsSelfLoop())
                    selfLoop = milkEdge;
            }

            // Test that a self-loop exists.
            Assert.IsNotNull(selfLoop);
            // Test that the self-loop is correct.
            Assert.AreEqual(0.1, selfLoop.TraversalProbability, 0.01);
        }

        /// <summary>
        /// Tests if a self-loop gets deleted if the sum of outgoing Edges
        /// equal to one and it is not needed. In other words, the user
        /// does not want a self-loop.
        /// </summary>
        [TestMethod]
        public void TestSelfLoopDelete()
        {
            MarkovChain<string> mc = new MarkovChain<string>();

            // Vertex and Edge objects are not meant to be accessed outside of the MarkovChain.
            // Hence the use of 'MarkovChain<string>.Vertex'.
            MarkovChain<string>.Vertex milk = mc.AddVertex("milk");
            MarkovChain<string>.Vertex eggs = mc.AddVertex("eggs");

            // A self-loop should be auto generated.
            mc.AddEdge(milk, eggs, 0.8);
            // The self-loop should be deleted.
            mc.AddEdge("milk", "beer", 0.2);

            HashSet<MarkovChain<string>.Edge> milkEdges = milk.AdjacentEdges();
            MarkovChain<string>.Edge selfLoop = null;

            // Check if a self-loop exists.
            foreach (MarkovChain<string>.Edge milkEdge in milkEdges)
            {
                if (milkEdge.IsSelfLoop())
                    selfLoop = milkEdge;
            }

            // Confirm that a self-loop has been erased.
            Assert.IsNull(selfLoop);
        }

        /// <summary>
        /// Tests the overrided Equals and HashCode methods,
        /// and methods AddVertex() and AddEdge() to test if items are
        /// being correctly added to the graph.
        /// </summary>
        [TestMethod]
        public void TestAddEquals()
        {
            MarkovChain<string> mc = new MarkovChain<string>();

            mc.AddEdge("milk", "eggs", 0.8);
            mc.AddEdge("milk", "bread", 0.1);

            // Only 3 objects should exist in the graph.
            Assert.AreEqual(3, mc.Vertices.Count);
        }

        /// <summary>
        /// Tests that the number of Edges is consistent with the
        /// Markov chain being created.
        /// </summary>
        [TestMethod]
        public void TestNumberOfEdges()
        {
            MarkovChain<string> mc = new MarkovChain<string>();

            // 3 Edges should be created here with 1 generated as a self-loop.
            mc.AddEdge("milk", "eggs", 0.8);
            mc.AddEdge("milk", "bread", 0.1);
            // 3 Edges should be created here with 1 generated as a self-loop.
            mc.AddEdge("eggs", "bread", 0.3);
            mc.AddEdge("eggs", "milk", 0.3);
            // 2 Edges should be created here.
            // No self-loop should be counted as probabilities add to 1.
            mc.AddEdge("bread", "milk", 0.5);
            mc.AddEdge("bread", "eggs", 0.5);

            // Exactly 8 Edges should exist in the graph.
            Assert.AreEqual(8, mc.Edges.Count);
        }
    }
}