using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class GizmoLine : MonoBehaviour
    {
        public Line line;
        public Color color = Color.white;

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

        public Vector3 GetPosition(float r)
        {
            return transform.TransformPoint( line.GetPosition(r));
        }
        public Vector3 GetDirection(float r)
        {
            return line.GetDirection(r);
        }
        
        private void DrawLine()
        {
            if (line == null)
                return;
            var l = line.AllLength;
            var s = l * line.startRatio;
            var e = l * line.endRatio;
            var colorStore = Gizmos.color;
            foreach (var pair in line.PairList)
            {
                var dt = (line.divideLength/pair.Length) * (pair.end - pair.start);
                for (float t = pair.start; t < pair.end; t+=dt)
                {
                    var p0 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, t));
                    var p1 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, t+dt));
                    if (debugColor)
                        Gizmos.color = Random.ColorHSV();
                    else
                        Gizmos.color = color;

                    Gizmos.DrawLine(p0,p1);
                }
            }
            Gizmos.color = colorStore;
        }
    }
}