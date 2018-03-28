using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.DataRenderer2D
{
    public struct Triangle : IMesh
    {
        Vertex _p0;
        Vertex _p1;
        Vertex _p2;
        public Triangle(Vertex p0, Vertex p1, Vertex p2)
        {
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
        }
        public IEnumerable<Vertex> Vertices
        {
            get
            {
                yield return _p0;
                yield return _p1;
                yield return _p2;
            }
        }

        public IEnumerable<int> Triangles
        {
            get
            {
                var list = new int[] { 0, 2, 1 };
                foreach (var number in list)
                    yield return number;
            }
        }
    }
}