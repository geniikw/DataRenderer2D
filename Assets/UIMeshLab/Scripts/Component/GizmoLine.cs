using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class GizmoLine : MonoBehaviour
    {
        public Line line;

        public bool isOnlyViewSelected = true;
        
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
                var divide = Mathf.Floor( pair.Length / line.divideRatido);
                for (int i = 0; i < divide; i++)
                {
                    var p0 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, 1f / divide * i));
                    var p1 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, 1f / divide * (i+1f)));
                    Gizmos.DrawLine(p0,p1);
                }
            }
        }
    }
}