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
        public float divideRatido = 1f;

        public float Length => PairList.Sum(p=>p.Length);

        public IEnumerable<LinePair> PairList {
            get
            {
                for (int i = 0; i < points.Count-1; i++)
                {
                    yield return new LinePair(points[i], points[i + 1]);
                }
                if (loop)
                    yield return new LinePair(points.Last(), points.First());
            }
        }
        public struct LinePair
        {
            public Node n0;
            public Node n1;
            public LinePair(Node n0, Node n1)
            {
                this.n0 = n0;
                this.n1 = n1;
            }
            public float Length => CurveLength.Auto(n0, n1);
        }
    }

  
}