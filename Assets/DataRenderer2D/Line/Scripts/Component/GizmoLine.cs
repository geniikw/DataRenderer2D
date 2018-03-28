using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    /// no render line.
    /// </summary>
    public class GizmoLine : MonoBehaviour
    {
        public Spline line;
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
            var colorStore = Gizmos.color;
            foreach (var pair in line.TargetPairList)
            {
                var dt = pair.GetDT(line.option.DivideLength);
                for (float t = pair.start; t < pair.end; t+=dt)
                {
                    var p0 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, t));
                    var p1 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, t+dt));
                    if (debugColor)
                        Gizmos.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    else
                        Gizmos.color = color;

                    Gizmos.DrawLine(p0,p1);
                }
            }
            Gizmos.color = colorStore;
        }
    }
}