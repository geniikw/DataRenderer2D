using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab.Polygon
{
    /// 인생은 짧고 내가 가야할 길은 멀다.

    public class CenterPolygon : IMeshDrawer
    {
        CircleCalculator _circle;
        IPolygon _target;

        public CenterPolygon(CircleCalculator cc, IPolygon target)
        {
            _circle = cc;
            _target = target;
        }
        
        public IEnumerable<IMesh> Draw()
        {
            List<Vertex> buffer = new List<Vertex>();
            for (int i = 0; i < _target.Polygon.count; i++)
            {
                var angle = 360f / _target.Polygon.count * i;
                buffer.Add(Vertex.New(
                    _circle.Calculate(angle),
                    _circle.CalculateUV(angle), 
                    _target.Polygon.color.Evaluate(angle/360)));
            }
            var aR = buffer.Average(vtx => vtx.color.r);
            var aB = buffer.Average(vtx => vtx.color.b);
            var aG = buffer.Average(vtx => vtx.color.g);
            var center = Vertex.New(Vector2.zero, Vector2.one * 0.5f, new Color(aR, aB, aG));


            for (int i = 0; i < _target.Polygon.count-1; i++)
            {
                yield return new Triangle(buffer[i], center, buffer[i+1]);
            }
            yield return new Triangle(buffer[_target.Polygon.count - 1], center, buffer[0]);
            

        }
    }
}