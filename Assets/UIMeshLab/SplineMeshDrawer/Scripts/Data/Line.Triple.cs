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
                if (Points.Count < 2)
                    yield break;

                var mode = option.mode;
                var sr = option.startRatio;
                var er = option.endRatio;
                var color = option.color;

                if (mode == LineOption.Mode.Loop && sr == 0f && er == 1f)
                    yield return new Triple(Points.Last(), Points.First(), Points[1], color.Evaluate(0));
                               
                var l = AllLength;
                var ls = sr * l;
                var le = er * l;
                var c = 0f;
                
                for (int i = 0; i < Points.Count-1; i++)
                {
                    c += CurveLength.Auto(Points[i], Points[i + 1]);
                    if(ls < c && c < le)
                    {
                        if (i == Points.Count - 1 && mode != LineOption.Mode.Loop)
                            break;

                        yield return new Triple(Points[i], Points[i + 1], Points[(i + 2)% Points.Count],color.Evaluate(c/l));
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