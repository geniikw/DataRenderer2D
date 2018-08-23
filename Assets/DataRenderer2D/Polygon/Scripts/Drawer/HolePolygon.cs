using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace geniikw.DataRenderer2D.Polygon
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
            var count = _target.Polygon.count;
            var startAngle = _target.Polygon.startAngle * 360f;
            var startFlag = false;
            var endAngle = _target.Polygon.endAngle * 360f;
            var endFlag = false;

            if (endAngle < startAngle)
                yield break;

            for (float i = 0; i < count; i++)
            {
                var unitAngle = 360f / count;

                var a0 = unitAngle * i;
                var a1 = unitAngle * (i + 1);

                if (startAngle > a1)
                    continue;
                
                var a = _circle.CalculateVertex(a0);
                var b = _circle.CalculateVertex(a1);
                var c = _circle.CalculateInnerVertex(a1);
                var d = _circle.CalculateInnerVertex(a0);
                
                var va = b.position - a.position;
              
                var ap = a.position;
                var bp = d.position;

                var uvV = b.uv - a.uv;
             
                var aUV = a.uv;
                var bUV = d.uv;
                if (!startFlag)
                {
      
                    startFlag = true;
                    a = _circle.CalculateVertex(startAngle);
                    d = _circle.CalculateInnerVertex(startAngle);

                    var vt = a.position;
                    var useless = Vector3.zero;
                    Intersect.ClosestPointsOnTwoLines(out a.position, out useless, Vector3.zero, vt, ap, va);
                    Intersect.ClosestPointsOnTwoLines(out d.position, out useless, Vector3.zero, vt, bp, va);

                    Vector3 aOut, bOut;
                   
                    Intersect.ClosestPointsOnTwoLines(out aOut, out useless, Vector3.one * 0.5f, a.uv - Vector2.one * 0.5f, aUV, uvV);
                    Intersect.ClosestPointsOnTwoLines(out bOut, out useless, Vector3.one * 0.5f, a.uv - Vector2.one * 0.5f, bUV, uvV);
                    a.uv = aOut;
                    d.uv = bOut;

                }

                if (!endFlag && endAngle < a1)
                {
                    endFlag = true;
                    b = _circle.CalculateVertex(endAngle);
                    c = _circle.CalculateInnerVertex(endAngle);

                    var vt = b.position;//-Vector3.zero;
                    var useless = Vector3.zero;
                    Intersect.ClosestPointsOnTwoLines(out b.position, out useless, Vector3.zero, vt, ap, va);
                    Intersect.ClosestPointsOnTwoLines(out c.position, out useless, Vector3.zero, vt, bp, va);

                    Vector3 aOut, bOut;

                    Intersect.ClosestPointsOnTwoLines(out aOut, out useless, Vector2.one * 0.5f, b.uv - Vector2.one * 0.5f, aUV, uvV);
                    Intersect.ClosestPointsOnTwoLines(out bOut, out useless, Vector2.one * 0.5f, b.uv - Vector2.one * 0.5f, bUV, uvV);
                    b.uv = aOut;
                    c.uv = bOut;
                }
                

                yield return new Triangle(a, b, c);

                if (_target.Polygon.innerRatio == 0f ||
                    _target.Polygon.type == PolygonType.Hole ||
                    _target.Polygon.type == PolygonType.HoleCurve ||
                    _target.Polygon.type == PolygonType.HoleCenterColor)
                    yield return new Triangle(a, c, d);

                if (endFlag)
                    yield break;

            }
        }
    }
}