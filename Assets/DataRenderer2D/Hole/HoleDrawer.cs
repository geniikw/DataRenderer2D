using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Hole
{
    public class HoleDrawer : IMeshDrawer
    {
        IUnitSize m_unit;
        IHole m_hole;
        
        public HoleDrawer(IUnitSize unit, IHole hole)
        {
            m_unit = unit;
            m_hole = hole;
        }
                
        public IEnumerable<IMesh> Draw()
        {
            HoleInfo hole = m_hole.Hole;

            Vector3 half = m_unit.Size / 2f;
            Vertex p0 = new Vertex(Vector3.zero-half, Vector2.zero, Color.white);
            Vertex p1 = new Vertex(Vector3.right * m_unit.Size.x - half, Vector2.right, Color.white);
            Vertex p2 = new Vertex(Vector3.up * m_unit.Size.y - half, Vector2.up, Color.white);
            Vertex p3 = new Vertex(half, Vector2.one, Color.white);

            var outer = new Vertex[] { p0, p1, p2, p3 };
            var inner = new Vertex[hole.Inner];

            var unit = Mathf.PI * 2f / hole.Inner;
            var angle = hole.Angle * Mathf.Deg2Rad;
            for (int i = 0; i < hole.Inner; i++)
            {
                var sin = Mathf.Sin(angle + unit * i) * Vector2.up / 2f * hole.Size.x;
                var cos = Mathf.Cos(angle + unit * i) * Vector2.right / 2f  * hole.Size.y;
                var uv = Vector2.one / 2f + sin + cos + hole.Offset;

                uv.x = Mathf.Clamp01(uv.x);
                uv.y = Mathf.Clamp01(uv.y);

                var y = (uv.y-0.5f) * m_unit.Size.y * Vector2.up;
                var x = (uv.x-0.5f)* m_unit.Size.x * Vector2.right;
                var pos = x + y;
                inner[i] = new Vertex(pos, uv, Color.white);
            }

            //yield return new Quad(p0,p1,p2,p3);            
            yield return new Triangle(inner[0], inner[1], inner[2]);
        }
    }
}