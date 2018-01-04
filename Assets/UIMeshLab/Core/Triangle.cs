using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.UIMeshLab
{
    public struct Triangle : IMesh
    {
        private Vertex[] m_vertices;

        public Triangle(Vertex v0, Vertex v1, Vertex v2)
        {
            m_vertices = new Vertex[3];
            m_vertices[0] = v0;
            m_vertices[1] = v1;
            m_vertices[2] = v2;
        }

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                foreach (var v in m_vertices)
                    yield return v;
            }
        }

        public IEnumerable<int> Triangles
        {
            get
            {
                return new int[] { 0, 1, 2 };
            }
        }
    }
}