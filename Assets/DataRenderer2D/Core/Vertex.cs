using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    public struct Vertex
    {
        public Vector3 position;
        public Vector2 uv;
        public Color   color;

        public Vertex(Vector3 pos, Vector2 u, Color c)
        {
            position = pos;
            uv = u;
            color = c;
        }
        public static Vertex New(Vector3 pos, Vector2 uv, Color color)
        {
            return new Vertex(pos, uv, color);
        }
        public static Vertex New(float x, float y, float z, float u, float v, Color color)
        {
            return new Vertex(new Vector3(x,y,z), new Vector2(u,v), color);
        }
    }
}