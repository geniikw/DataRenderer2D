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
            var count = _target.Polygon.count;
                       
            for (int i = 0; i < count; i++)
            {
                var unitAngle = 360f / count;

                yield return new Triangle(
                    _circle.CalculateVertex(unitAngle * i),
                    _circle.CalculateInnerVertex(unitAngle * ((i + 1) % count)),
                    _circle.CalculateVertex(unitAngle * ((i + 1) % count)));

                if (_target.Polygon.innerRatio == 0f ||
                    _target.Polygon.type == PolygonType.Hole ||
                    _target.Polygon.type == PolygonType.HoleCenterColor)
                    yield return new Triangle(
                       _circle.CalculateVertex(unitAngle * i),
                       _circle.CalculateInnerVertex(unitAngle * i),
                       _circle.CalculateInnerVertex(unitAngle * ((i + 1) % count)));
            }
        }
    }
}