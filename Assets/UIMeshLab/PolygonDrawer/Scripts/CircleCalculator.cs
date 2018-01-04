using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon
{
    public class CircleCalculator {
        readonly IPolygon _target;
        readonly IUnitSizer _unit;

        public CircleCalculator(IPolygon target, IUnitSizer size)
        {
            _target = target;
            _unit = size;
        }

        public Vector3 Calculate(float angle)
        {
            angle *= Mathf.Deg2Rad;
            return _target.Polygon.cosCft * Mathf.Cos(angle) * Vector3.right * _unit.Size.x * 0.5f
                 + _target.Polygon.sinCft * Mathf.Sin(angle) * Vector3.up * _unit.Size.y * 0.5f;
        }

        public Vector2 CalculateUV(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * 0.5f + 0.5f, Mathf.Sin(angle * Mathf.Deg2Rad) * 0.5f + 0.5f);
        }

    }
    public interface IUnitSizer
    {
        Vector2 Size { get;}
    }
}