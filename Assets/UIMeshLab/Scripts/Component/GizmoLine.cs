using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class GizmoLine : MonoBehaviour
    {
        public Line line;

        public bool isOnlyViewSelected = true;

        public bool debugColor = false;

        private void OnDrawGizmos()
        {
            if (isOnlyViewSelected)
                return;
            DrawLine();
        }

        private void OnDrawGizmosSelected()
        {
            if (!isOnlyViewSelected)
                return;
            DrawLine();
        }

        private void DrawLine()
        {
            if (line == null)
                return;
           
            var l = line.Length;
            var s = l * line.startRatio;
            var e = l * line.endRatio;
            foreach (var pair in line.PairList)
            {
                var dt = (line.divideLength/pair.Length) * (pair.end - pair.start);
                for (float t = pair.start; t < pair.end; t+=dt)
                {
                    var p0 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, t));
                    var p1 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, t+dt));
                    if(debugColor)
                        Gizmos.color = Random.ColorHSV();
                    Gizmos.DrawLine(p0,p1);
                }
            }
        }
    }
}