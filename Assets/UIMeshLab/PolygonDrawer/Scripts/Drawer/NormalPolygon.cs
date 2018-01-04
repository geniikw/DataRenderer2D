using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon
{
    public class NormalDrawer : IMeshDrawer
    {
        CircleCalculator _circle;
        IPolygon _target;

        public NormalDrawer(CircleCalculator cc, IPolygon target)
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
                buffer.Add(Vertex.New(_circle.Calculate(angle),_circle.CalculateUV(angle), _target.Polygon.color.Evaluate(angle/360)));
            }

            int[] v = new int[3] { 0, 1, _target.Polygon.count - 1 };
            int n = 0;
            while(n < _target.Polygon.count - 2)
            {
                if (n % 2 == 1)
                    yield return new Triangle(buffer[v[0]], buffer[v[1]], buffer[v[2]]);
                else
                    yield return new Triangle(buffer[v[0]], buffer[v[2]], buffer[v[1]]);

                v[n % 3] += (v[n % 3] == 0) ? 2 : (_target.Polygon.count - 2 - n) * (n % 2 == 1 ? 1 : -1);
                n++;
            }
        }
    }
}