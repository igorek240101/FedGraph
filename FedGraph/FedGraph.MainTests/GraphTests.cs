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
    public class GraphTests
    {
        private string filename = "config_test.json";
        private Config TestConfig;
        private Graph TestGraph;
        [TestInitialize]
        public void Init()
        {
            TestConfig = Parsing.parse(filename);
            TestGraph = new Graph(TestConfig);
        }
        [TestMethod()]
        public void getVertexesNumTest_9()
        {
            int expect = 9;
            int actual = TestGraph.getVertexesNum();
            Assert.AreEqual(expect, actual);
        }

        [TestMethod()]
        public void fillMatrixTest_0_1_1()
        {
            int actual = TestGraph.getEdgeWeight(0, 1);
            int expect = 1;
            Assert.AreEqual(expect, actual); 
        }
        [TestMethod()]
        public void fillMatrixTest_0_3_5()
        {
            int actual = TestGraph.getEdgeWeight(0, 3);
            int expect = 5;
            Assert.AreEqual(expect, actual); 
        }
        [TestMethod()]
        public void fillMatrixTest_1_2_4()
        {
            int actual = TestGraph.getEdgeWeight(2, 1);
            int expect = 4;
            Assert.AreEqual(expect, actual);
        }
        [TestMethod()]
        public void fillMatrixTest_4_7_neg1()
        {
            int actual = TestGraph.getEdgeWeight(4, 7);
            int expect = -1;
            Assert.AreEqual(expect, actual);
        }
        
    }
}