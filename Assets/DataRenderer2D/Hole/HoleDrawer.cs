using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.DataRenderer2D.Hole
{
    public struct Pair<T> 
    {
        public T first;
        public T second; 
    }

    public static class ListUtil
    {
        public static IEnumerable<Pair<T>> Pairloop<T>(this IEnumerable<T> source)
        {
            T previous = default(T);
            using (var it = source.GetEnumerator())
            {
                if (it.MoveNext())
                    previous = it.Current;
                var ff = previous;
                while (it.MoveNext())
                {
                    yield return new Pair<T>
                    {
                        first = previous,
                        second = it.Current
                    };
                    previous = it.Current;
                }
                yield return new Pair<T> {
                    first = previous,
                    second = ff
                };
            }
        }

        public static Vertex FindNearst(this Vertex[] source, Vector3 target, Vector3 dir)
        {
            var dest = float.MaxValue;
            Vertex result = default(Vertex); 
            
            foreach(var v in source)
            {
                var pos = v.position;
                var d = (pos - target).normalized;
                var f = Vector3.Dot(d, dir);
                var calc = Vector3.Distance(target, pos) * f;

                if (dest > calc)
                {
                    dest = calc;
                    result = v;
                }
            }

            return result;
        }

    }


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
            var color = hole.Color;
            Vector3 half = m_unit.Size / 2f;
            Vertex p0 = new Vertex(Vector3.zero-half, Vector2.zero, color);
            Vertex p1 = new Vertex(Vector3.right * m_unit.Size.x - half, Vector2.right, color);
            Vertex p2 = new Vertex(Vector3.up * m_unit.Size.y - half, Vector2.up, color);
            Vertex p3 = new Vertex(half, Vector2.one, color);

            var outer = new Vertex[] { p0, p1, p2, p3 };
            var inner = new Vertex[hole.Inner];

            var unit = Mathf.PI * 2f / hole.Inner;
            var angle = hole.Angle * Mathf.Deg2Rad;
            for (int i = 0; i < hole.Inner; i++)
            {
                var sin = Mathf.Sin(angle + unit * i) * Vector2.up / 2f * hole.SizeX;
                var cos = Mathf.Cos(angle + unit * i) * Vector2.right / 2f  * hole.SizeY;
                var uv = Vector2.one / 2f + sin + cos + new Vector2( hole.OffsetX, hole.OffsetY);

                uv.x = Mathf.Clamp01(uv.x);
                uv.y = Mathf.Clamp01(uv.y);

                var y = (uv.y-0.5f) * m_unit.Size.y * Vector2.up;
                var x = (uv.x-0.5f)* m_unit.Size.x * Vector2.right;
                var pos = x + y;
                inner[i] = new Vertex(pos, uv, color);
            }

            foreach(var p in inner.Pairloop())
            {
                var c = (p.first.position + p.second.position) / 2f;

                var normal = Vector3.Cross(p.first.position - p.second.position, Vector3.forward).normalized;

                var n = outer.FindNearst(c, normal);

                yield return new Triangle(p.first, n, p.second );
            }

            foreach(var p in new Pair<Vertex>[] {   new Pair<Vertex>{first= p1, second = p0 },
                                                    new Pair<Vertex>{first= p0, second = p2 },
                                                    new Pair<Vertex>{first= p3, second = p1 },
                                                    new Pair<Vertex>{first= p2, second = p3 }}){
                var c = (p.first.position + p.second.position) / 2f;
                var normal = -Vector3.Cross(p.first.position - p.second.position, Vector3.forward).normalized;

                var n = inner.FindNearst(c,normal);

                yield return new Triangle(p.first, n, p.second);
            }

            //yield return new Quad(p0,p1,p2,p3);            
            //yield return new Triangle(inner[0], inner[1], inner[2]);
        }
    }
}