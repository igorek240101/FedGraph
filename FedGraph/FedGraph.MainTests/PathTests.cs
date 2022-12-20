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
    public class PathTests
    {
        private Path prev;
        private Vertex TestVertex;
        [TestInitialize]
        public void Init() {
            TestVertex = new Vertex(0, "0");
            prev = new Path(TestVertex, 0, null);
        }
        [TestMethod()]
        public void PathTest()
        {
            int expected = 10;
            int newWiight = 10;
            Vertex NewVertex = new Vertex(1, "1");
            prev = new Path(NewVertex, prev.getMinLength()+newWiight, prev);
            Assert.AreEqual(expected, prev.getMinLength());
        }
    }
}