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
    ///          bool loop;
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

        public float Length => m_pairList.Sum(p => p.Length);

        private Queue<LinePair> m_pairList = new Queue<LinePair>(); 
        public Queue<LinePair> PairList {
            get
            {
                m_pairList.Clear();
                for (int i = 0; i < points.Count - 1; i++)
                {
                    var n0 = points[i];
                    var n1 = points[i + 1];
                    m_pairList.Enqueue(new LinePair(n0, n1));
                }

                if (points[points.Count - 1].loop)
                    m_pairList.Enqueue(new LinePair(points[points.Count - 1], points[0]));

                return m_pairList;
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