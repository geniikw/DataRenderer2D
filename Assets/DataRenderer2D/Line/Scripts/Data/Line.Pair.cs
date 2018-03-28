using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    /// to draw bezier, define IEnumerable<LinePair>
    /// </summary>
    public partial struct Spline
    {
        IEnumerable<Point[]> AllPair
        {
            get
            {
                bool first = true;
                Point firstPoint = Point.Zero;
                Point lastPoint = Point.Zero;
                Point prev = Point.Zero;
                int count = 0;

                foreach(var p in this)
                {
                    count++;
                    if (first)
                    {
                        firstPoint = p;
                        lastPoint = p;
                        first = false;
                        continue;
                    }

                    yield return new Point[] {lastPoint, p};

                    lastPoint = p;
                }
                              
                if (option.mode == LineOption.Mode.Loop && count > 1)
                {
                    yield return new Point[] { lastPoint, firstPoint };
                }
            }
        }

        public IEnumerable<LinePair> TargetPairList
        {
            get
            {
                var l = AllLength;
                var ls = l * option.startRatio;
                var le = l * option.endRatio;
                var ps = 0f;
                var pe = 0f;
                var pl = 0f;

                if (ls >= le)
                    yield break;

                foreach (var pair in AllPair)
                {
                    pl = CurveLength.Auto(pair[0], pair[1]);
                    pe = ps + pl;

                    if (le < ps)
                        yield break;
                    if (ls < pe)
                        yield return new LinePair(pair[0], pair[1], Mathf.Max(0f, (ls - ps) / pl), Mathf.Min(1f, (le - ps) / pl), ps / l, pe / l);
                    ps = pe;
                }
            }
        }
        [Serializable]
        public struct LinePair
        {
            public Point n0;
            public Point n1;
            [NonSerialized]
            public float sRatio;
            [NonSerialized]
            public float eRatio;
            public float RatioLength
            {
                get
                {
                    return eRatio - sRatio;
                }
            }
            [NonSerialized]
            public float start;
            [NonSerialized]
            public float end;
            public LinePair(Point n0, Point n1, float s, float e, float sr, float er)
            {
                this.n0 = n0;
                this.n1 = n1;
                start = s;
                end = e;
                sRatio = sr;
                eRatio = er;
            }
            public float Length
            {
                get
                {
                    return CurveLength.Auto(n0, n1) * (end - start);
                }
            }

            public float GetDT(float divideLength)
            {
                return (divideLength / Length) * (end - start);
            }
            public Vector3 GetPoisition(float r)
            {
                return Curve.Auto(n0, n1, Mathf.Lerp(start, end, r));
            }
            public Vector3 GetDirection(float r)
            {
                return Curve.AutoDirection(n0, n1, Mathf.Lerp(start, end, r));
            }
            public float GetWidth(float t)
            {
                return Mathf.Lerp(n0.width, n1.width, Mathf.Lerp(start, end, t));
            }
        }
    }
}