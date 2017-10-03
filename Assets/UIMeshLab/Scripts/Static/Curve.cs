using UnityEngine;

namespace geniikw.UIMeshLab
{
    public static class Curve
    {
        public static Vector3 Cubic(Transform owner, Node n0, Node n1, float t) 
        {
            var p0 = owner.TransformPoint(n0.position);
            var c0 = owner.TransformPoint(n0.NextControlPosition);
            var c1 = owner.TransformPoint(n1.PreviousControlPoisition);
            var p1 = owner.TransformPoint(n1.position);
            return Cubic(p0, c0, c1, p1, t);
        }
        
        public static Vector3 CubicDirection(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float mt = 1f - t;
            return (3f * mt * mt * (p1 - p0) + 6f * mt * t * (p2 - p1) + 3f * t * t * (p3 - p2)).normalized;
        }  
        public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float mt = 1f - t;
            return p0 * mt * mt * mt + 3f * p1 * mt * mt * t + 3f * p2 * mt * t * t + p3 * t * t * t;
        }

        /* it doesn't use
        public static Vector3 QuadraticDirection(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float mt = 1f - t;
            return 2f * mt * (p1 - p0) + 2f * t * (p2 - p1);
        }

        public static Vector3 Quadratic(Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float mt = 1f - t;
            return p1 * mt * mt + 2f * p2 * mt * t + p3 * t * t;
        }
        */

    }
}