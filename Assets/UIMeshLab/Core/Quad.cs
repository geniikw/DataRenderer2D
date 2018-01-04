using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.UIMeshLab
{
    public struct Quad : IMesh
    {
        private Vertex[] m_vertices;
        
        public Quad(Vertex v0, Vertex v1, Vertex v2, Vertex v3)
        {
            m_vertices = new Vertex[4];
            m_vertices[0] = v0;
            m_vertices[1] = v1;
            m_vertices[2] = v2;
            m_vertices[3] = v3;
        }

        public Quad(Vector2 size, Vector2 center, Color color)
        {
            m_vertices = new Vertex[4];
            m_vertices[0] = Vertex.New(center - new Vector2(-size.x, -size.y) / 2f, new Vector2(0, 0), color);
            m_vertices[1] = Vertex.New(center - new Vector2(-size.x, size.y) / 2f, new Vector2(0, 0), color);
            m_vertices[2] = Vertex.New(center - new Vector2(size.x, size.y) / 2f, new Vector2(0, 0), color);
            m_vertices[3] = Vertex.New(center - new Vector2(size.x, -size.y) / 2f, new Vector2(0, 0), color);
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
                return new int[] { 0, 1, 3, 1, 2, 3 };
            }
        }
    }
}