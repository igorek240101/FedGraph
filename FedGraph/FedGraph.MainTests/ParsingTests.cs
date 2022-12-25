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
    public class ParsingTests
    {
        private string filename = "config_test.json";
        private Config TestConfig;
        [TestInitialize]
        public void Init()
        {
            TestConfig= Parsing.parse(filename);
        }
        [TestMethod()]
        public void parseTest_Id_Vertex9()
        {
            CVertex actual_vertex;
            int actual_id;
            int expected_id;
            for (int i=0; i<9; i++)
            {
                actual_vertex = TestConfig.vertexes[i];
                actual_id = actual_vertex.id;
                expected_id = i + 1;
                Assert.AreEqual(expected_id, actual_id);
            }
        }
        [TestMethod()]
        public void parseTest_AdjList_Vertex1() 
        {
            int vertex_id = 0;
            CAdjVertex actual_list;
            actual_list = TestConfig.adj_list[vertex_id];
            Assert.AreEqual(vertex_id+1, actual_list.id);

            int exp_id_1 = 2, exp_weight_1 = 1;
            int act_id_1 = actual_list.edges[0].id;
            int act_weight_1 = actual_list.edges[0].weight;
            Assert.AreEqual(exp_id_1, act_id_1);
            Assert.AreEqual(exp_weight_1, act_weight_1);

            int exp_id_2 = 3, exp_weight_2 = 3;
            int act_id_2 = actual_list.edges[1].id;
            int act_weight_2 = actual_list.edges[1].weight;
            Assert.AreEqual(exp_id_1, act_id_1);
            Assert.AreEqual(exp_weight_1, act_weight_1);

            int exp_id_3 = 4, exp_weight_3 = 5;
            int act_id_3 = actual_list.edges[2].id;
            int act_weight_3 = actual_list.edges[2].weight;
            Assert.AreEqual(exp_id_1, act_id_1);
            Assert.AreEqual(exp_weight_1, act_weight_1);

        }
    }
}