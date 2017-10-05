using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class NoRenderLine : MonoBehaviour
    {
        public Line line;

        public bool isViewSelected = true;
        
        private void OnDrawGizmos()
        {
            if (isViewSelected)
                return;
            DrawLine();
        }

        private void OnDrawGizmosSelected()
        {
            if (!isViewSelected)
                return;
            DrawLine();
        }

        private void DrawLine()
        {
            if (line == null)
                return;

            foreach (var pair in line.PairList)
            {
                for (int i = 0; i < pair.n0.DivideCount; i++)
                {
                    var p0 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, 1f / pair.n0.DivideCount * i));
                    var p1 = transform.TransformPoint(Curve.Auto(pair.n0, pair.n1, 1f / pair.n0.DivideCount * (i+1f)));
                    Gizmos.DrawLine(p0,p1);
                }
            }

        }
    }
}