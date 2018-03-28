using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Polygon
{
    public class CircleCalculator {
        readonly IPolygon _target;
        readonly IUnitSize _unit;

        public CircleCalculator(IPolygon target, IUnitSize size)
        {
            _target = target;
            _unit = size;
        }

        private Vector3 Calculate(float angle)
        {
            var factor = _target.Polygon.type == PolygonType.HoleCurve ? _target.Polygon.curve.Evaluate(angle / 360f) : 1f; 
            angle *= Mathf.Deg2Rad;
            return _target.Polygon.cosCft * Mathf.Cos(angle) * Vector3.right * _unit.Size.x * 0.5f* factor
                 + _target.Polygon.sinCft * Mathf.Sin(angle) * Vector3.up * _unit.Size.y * 0.5f* factor;
        }

        private Vector2 CalculateUV(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * 0.5f + 0.5f, Mathf.Sin(angle * Mathf.Deg2Rad) * 0.5f + 0.5f);
        }

        private Vector2 CalculateInnerUV(float angle)
        {
            return new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * 0.5f * _target.Polygon.innerRatio + 0.5f, 
                Mathf.Sin(angle * Mathf.Deg2Rad) * 0.5f * _target.Polygon.innerRatio + 0.5f);
        }

        public Vertex CalculateVertex(float angle)
        {
            return Vertex.New(
                Calculate(angle),
                CalculateUV(angle),
                _target.Polygon.outerColor.Evaluate(angle / 360f));
        }

        public Vertex CalculateInnerVertex(float angle)
        {
            return Vertex.New(
               Calculate(angle) * _target.Polygon.innerRatio,
               CalculateInnerUV(angle),
               _target.Polygon.type >= PolygonType.HoleCenterColor? _target.Polygon.centerColor: _target.Polygon.outerColor.Evaluate(angle / 360f));
        }
        

    }
}