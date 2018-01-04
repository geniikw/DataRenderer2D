using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab.Polygon
{
    public class HolePolygon : IMeshDrawer
    {
        CircleCalculator _circle;
        IPolygon _target;

        public HolePolygon(CircleCalculator cc, IPolygon target)
        {
            _circle = cc;
            _target = target;
        }
        
        public IEnumerable<IMesh> Draw()
        {
            List<Vertex> buffer = new List<Vertex>();
            var count = _target.Polygon.count;
            for (int i = 0; i < count; i++)
            {
                var angle = 360f / count * i;
                buffer.Add(Vertex.New(_circle.Calculate(angle),_circle.CalculateUV(angle), _target.Polygon.color.Evaluate(angle/360)));
            }
            var aR = buffer.Average(vtx => vtx.color.r);
            var aB = buffer.Average(vtx => vtx.color.b);
            var aG = buffer.Average(vtx => vtx.color.g);
         
            for (int i = 0; i < count; i++)
            {
                var iv = buffer[(1 + i) % count];
                iv.position *= _target.Polygon.innerRatio;
                iv.uv -= Vector2.one / 2f;
                iv.uv *= _target.Polygon.innerRatio;
                iv.uv += Vector2.one / 2f;
                iv.color = new Color(aR, aB, aG);

                var iv2 = buffer[i];
                iv2.position *= _target.Polygon.innerRatio;
                iv2.uv -= Vector2.one / 2f;
                iv2.uv *= _target.Polygon.innerRatio;
                iv2.uv += Vector2.one / 2f;
                iv2.color = new Color(aR, aB, aG);

                yield return new Triangle(buffer[i], iv, buffer[(i + 1)% count]);
                if (_target.Polygon.type != PolygonType.HoleHalf)
                    yield return new Triangle(buffer[i], iv2, iv);
            }
        }
    }
}