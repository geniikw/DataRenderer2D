using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    public partial class Line 
    {
        public IEnumerable<Triple> TripleList
        {
            get
            {
                if (points.Count < 2)
                    yield break;
                
                if (loop && startRatio == 0f && endRatio == 1f)
                    yield return new Triple(points.Last(), points.First(), points[1]);

                var l = AllLength;
                var ls = startRatio * l;
                var le = endRatio * l;
                var c = 0f;
                
                for (int i = 0; i < points.Count-1; i++)
                {
                    c += CurveLength.Auto(points[i], points[i + 1]);
                    if(ls < c && c < le)
                        yield return new Triple(points[i], points[i + 1], points[(i + 2)%points.Count]);
                }
            }
        }
        
        public struct Triple
        {
            Node previous;
            Node target;
            Node next;

            public Triple(Node p, Node c, Node n)
            {
                previous = p; target = c; next = n;
            }

            public Vector3 ForwardDirection => Curve.AutoDirection(target, next, 0.01f);
            public Vector3 BackDirection => Curve.AutoDirection(previous, target , 0.99f);
            public Vector3 Position => target.position;
            public float CurrentWidth => target.width;
            public Color CurrentColor => target.color;
        }
    }
}