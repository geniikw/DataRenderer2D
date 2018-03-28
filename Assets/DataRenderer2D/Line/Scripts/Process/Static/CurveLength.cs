using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D {
    
    public static class CurveLength {
        /// <summary>
        /// for test
        /// </summary>
        public static float SumDirections(Point n0, Point n1)
        {
            return SumDirections(n0.position, n0.NextControlPosition, n1.PreviousControlPoisition, n1.position);
        }
        /// <summary>
        /// for test
        /// </summary>
        public static float SumDirections(Vector3 p0, Vector3 c0, Vector3 c1, Vector3 p1)
        {
            var length = 0f;
            var dt = 0.01f;
            for (float t = dt; t < 1f; t += dt)
            {
                var fst = Curve.Auto(p0,c0,c1,p1, t - dt);
                var sec = Curve.Auto(p0,c0,c1,p1, t);

                length += Vector3.Distance(fst, sec);
            }
            return length;
        }


        public static float Auto(Point n0, Point n1)
        {
            return Auto(n0.position, n0.NextControlPosition, n1.PreviousControlPoisition, n1.position);
        }
        public static float Auto(Vector3 p0, Vector3 c0, Vector3 c1, Vector3 p1)
        {
            if (c0 == p0 && p1 == c1)
                return Vector3.Distance(p0, p1);
            if (c0 == p0 || c1 == p1)
                return Quadratic(p0, c0==p0 ? c1 : c0  , p1);

            return QuadraticApproximation(p0, c0, c1, p1);
        }

        /// <summary>
        /// ref : https://github.com/HTD/FastBezier/blob/master/Program.cs
        /// </summary>
        static float Quadratic(Vector3 p0, Vector3 cp, Vector3 p1)
        {
            if (p0 == p1)
                if (p0 == cp)
                    return 0f;
                else
                    return Vector3.Distance(p0, cp);
            if (p0 == cp || p1 == cp)
                return Vector3.Distance(p0, p1);

            Vector3 A0 = cp - p0;
            Vector3 A1 = p0 - 2.0f * cp + p1;

            if (A1 == Vector3.zero)
                return 2.0f * A0.magnitude;

            var c = 4f * Vector3.Dot(A1, A1);
            var b = 8f * Vector3.Dot(A0, A1);
            var a = 4f * Vector3.Dot(A0, A0);
            var q = 4f * a * c - b * b;
            var twoCpB = 2f * c + b;
            var sumCBA = c + b + a;
            var l0 = (0.25f / c) * (twoCpB * Mathf.Sqrt(sumCBA) - b * Mathf.Sqrt(a));

            if (q == 0f)
                return l0;

            var l1 = (q / (8f * Mathf.Pow(c, 1.5f))) * (Mathf.Log(2.0f * Mathf.Sqrt(c * sumCBA) + twoCpB)- Mathf.Log(2.0f * Mathf.Sqrt(c * a) + b));
            return l0 + l1;
        }
        
        static float QuadraticApproximation(Vector3 p0, Vector3 c0, Vector3 c1, Vector3 p1)
        {
            return Quadratic(p0, (3f * c1 - p1 + 3f * c0 - p0)/4f, p1);
        }

    }
}