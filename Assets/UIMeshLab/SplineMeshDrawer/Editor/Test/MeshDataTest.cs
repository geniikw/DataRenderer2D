#if TEST_ENABLE
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace geniikw.UIMeshLab.Test
{

    public class MeshDataTest {     
        
        [Test]
        public void AddTest()
        {
            var A = MeshData.Quad(Vertex.New(0, 0, 0, 0, 0, Color.white),
                                  Vertex.New(0, 1, 0, 0, 1, Color.white),
                                  Vertex.New(1, 1, 0, 1, 1, Color.white),
                                  Vertex.New(1, 0, 0, 1, 0, Color.white));

            var A2 = A + A;

            Assert.AreEqual(12, A2.triangles.Count);
            Assert.AreEqual(8, A2.vertexes.Count);
        }


    }
}
#endif