using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.DataRenderer2D
{
    public struct Quad : IMesh
    {
        Vertex _p0;
        Vertex _p1;
        Vertex _p2;
        Vertex _p3;


        public Quad(Vertex p0, Vertex p1, Vertex p2, Vertex p3)
        {
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
        }

        public Quad(Vector2 size, Vector2 center, Color color)
        {
            _p0 = Vertex.New(center - new Vector2(-size.x, -size.y) / 2f, new Vector2(0, 0), color);
            _p1 = Vertex.New(center - new Vector2(-size.x, size.y) / 2f, new Vector2(0, 0), color);
            _p2 = Vertex.New(center - new Vector2(size.x, size.y) / 2f, new Vector2(0, 0), color);
            _p3 = Vertex.New(center - new Vector2(size.x, -size.y) / 2f, new Vector2(0, 0), color);
        }

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                yield return _p0;
                yield return _p1;
                yield return _p2;
                yield return _p3;

            }
        }

        public IEnumerable<int> Triangles
        {
            get
            {
                var list = new int[] { 0, 2, 1, 1, 2, 3 };
                foreach (var number in list)
                    yield return number;
            }
        }
    }
}