using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace geniikw.UIMeshLab
{
    public partial class Line
    {
        IEnumerable<Node[]> Pair
        {
            get
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    yield return new Node[] { points[i], points[i + 1] };
                }
                if (loop && points.Count > 1)
                {
                    yield return new Node[] { points.Last(), points.First() };
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
                        yield return new LinePair(pair[0], pair[1], Mathf.Max(0f, (ls - ps) / pl), Mathf.Min(1f, (le - ps) / pl));
                    ps = pe;
                }
            }
        }

        public struct LinePair
        {
            public Node n0;
            public Node n1;
            public float start;
            public float end;
            public LinePair(Node n0, Node n1, float s, float e)
            {
                this.n0 = n0;
                this.n1 = n1;
                start = s;
                end = e;
            }
            public float Length => CurveLength.Auto(n0, n1) * (end - start);

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
        }
    }
}