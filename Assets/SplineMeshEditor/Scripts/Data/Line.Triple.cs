using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    /// <summary>
    /// to draw joint, Define IEnumerable<Triple>
    /// </summary>
    public partial struct Spline 
    {
        public IEnumerable<Triple> TripleList
        {
            get
            {
                if (points.Count < 2)
                    yield break;

                if (mode == Mode.Loop && startRatio == 0f && endRatio == 1f)
                    yield return new Triple(points.Last(), points.First(), points[1], color.Evaluate(0));
                               
                var l = AllLength;
                var ls = startRatio * l;
                var le = endRatio * l;
                var c = 0f;
                
                for (int i = 0; i < points.Count-1; i++)
                {
                    c += CurveLength.Auto(points[i], points[i + 1]);
                    if(ls < c && c < le)
                    {
                        if (i == points.Count - 1 && mode != Mode.Loop)
                            break;

                        yield return new Triple(points[i], points[i + 1], points[(i + 2)%points.Count],color.Evaluate(c/l));
                    }
                }
            }
        }

        public struct Triple
        {
            Point previous;
            Point target;
            Point next;
            Color color;

            public Triple(Point p, Point c, Point n, Color cl)
            {
                previous = p; target = c; next = n; color = cl;
            }

            public Vector3 ForwardDirection {
                get
                {
                    return Curve.AutoDirection(target, next, 0f);
                }
            }
            public Vector3 BackDirection
            {
                get
                {
                    return Curve.AutoDirection(previous, target, 1f);
                }
            }
            public Vector3 Position
            {
                get
                {
                    return target.position;
                }
            }
            public float CurrentWidth
            {
                get
                {
                    return target.width;
                }
            }
            public Color CurrentColor
            {
                get
                {
                    return color;
                }
            }
        }
    }
}