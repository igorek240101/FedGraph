using Microsoft.VisualStudio.TestTools.UnitTesting;
using FedGraph.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedGraph.Main.Tests
{
    [TestClass()]
    public class VertexTests
    {
        private Vertex TestVertex;
        [TestInitialize]
        public void Init() {
            int id = 0;
            string info = "Test";
            TestVertex = new Vertex(id,info);

        }

        [TestMethod()]
        public void addAdjVertexTest_addone()
        {
            int expect_id = 0;
            TestVertex.addAdjVertex(expect_id);
            int actual = TestVertex.getAdjVertex(expect_id);
            Assert.AreEqual(expect_id, actual);
        }
        [TestMethod()]
        public void addAdjVertexTest_addmany()
        {
            for (int i = 0; i<=100; i++)
            {
                TestVertex.addAdjVertex(i);
                int actual = TestVertex.getAdjVertex(i);
                Assert.AreEqual(i, actual);
            }
            
        }
    }
}