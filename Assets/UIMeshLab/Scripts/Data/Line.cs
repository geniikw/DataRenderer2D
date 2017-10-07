using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    /// Line 
    ///     List<Node> points 
    ///          Vector3 position;
    ///          Vector3 previousCurvePosition;
    ///          Vector3 nextCurvePosition;
    ///          Color color;
    ///          float width;
    ///          float angle;
    ///          int nextDivieCount;
    ///     bool loop;
    ///     float startRatio
    ///     float endRatio

    [Serializable]
    public partial class Line
    {
        public List<Node> points = new List<Node>();
        [Range(0, 1)]
        public float startRatio = 0f;
        [Range(0, 1)]
        public float endRatio = 1f;
        public bool loop = false;
        [Range(1,100)]
        public float divideLength = 1f;

        public Vector3 GetPosition(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);

            var cl = ratio * Length;
            
            foreach(var pair in PairList)
            {
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    return pair.GetPoisition(cl / pair.Length);
            }
            return points.Last().position;
        }
        
        public Vector3 GetDirection(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            var cl = ratio * Length;

            foreach (var pair in PairList)
            {
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    return pair.GetDirection(cl / pair.Length);
            }
            throw new Exception("ㅇㅇㅇ?");
        }
        
        /// <summary>
        /// O(n)
        /// </summary>
        public float AllLength
        {
            get
            {
                var length = 0f;
                if (points.Count > 1)
                   for (int i = 0; i < points.Count - 1; i++)
                        length += CurveLength.Auto(points[i], points[i + 1]);
                if (loop)
                    length += CurveLength.Auto(points.Last(), points[0]);
                return length;
            }
        }

        public float Length => PairList.Sum(p => p.Length);

        IEnumerable<Node[]> Pair
        {
            get
            {
                for (int i = 0; i < points.Count-1; i++)
                {
                    yield return new Node[] { points[i], points[i + 1] };
                }
                if (loop)
                {
                    yield return new Node[] { points.Last(), points.First() };
                }
            }
        }

        public IEnumerable<LinePair> PairList {
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

                foreach(var pair in Pair)
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
            public float Length => CurveLength.Auto(n0, n1)*(end - start);
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