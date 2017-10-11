﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace geniikw.UIMeshLab
{
    /// <summary>
    /// to draw bezier, define IEnumerable<LinePair>
    /// </summary>
    public partial struct Spline
    {
        IEnumerable<Point[]> Pair
        {
            get
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    yield return new Point[] { points[i], points[i + 1] };
                }
                if (mode == Mode.Loop && points.Count > 1)
                {
                    yield return new Point[] { points.Last(), points.First() };
                }
            }
        }

        public IEnumerable<LinePair> PairList
        {
            get
            {
                var l = AllLength;
                var ls = l * startRatio;
                var le = l * endRatio;
                var ps = 0f;
                var pe = 0f;
                var pl = 0f;

                if (ls >= le)
                    yield break;

                foreach (var pair in Pair)
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

        public struct LinePair
        {
            public Point n0;
            public Point n1;

            public float sRatio;
            public float eRatio;
            public float RatioLength
            {
                get
                {
                    return eRatio - sRatio;
                }
            }

            public float start;
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