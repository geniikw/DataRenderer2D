using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{

    /// <summary>
    /// data container of spline.
    /// define struct to use animator.
    /// and define ISpline to use like pointer.
    /// </summary>

    [Serializable]
    public partial struct Spline
    {
        public enum Mode
        {
            Noraml = 0,
            Loop = 1,
            RoundEdge = 2
        }
        [HideInInspector]
        [Header("it's valid when useListPoint is false")]
        [Tooltip("list can't animate, so if you want to animate point, set false")]
        public bool splineMode;
        [Header("it's valid when SplineMode is false")]
        public Point p0;
        [Header("it's valid when SplineMode is false")]
        public Point p1;
        [Header("it's valid when SplineMode is true")]
        public List<Point> points;
        
        public IList<Point> Points
        {
            get
            {
                if (splineMode)
                    return points;
                else
                    return new Point[] { p0, p1 };
            }
        }

        public LineOption option;

        public static Spline Default
        {
            get
            {
                return new Spline
                {
                    points = new List<Point>(),
                    splineMode = true,
                    p0 = Point.Zero,
                    p1 = new Point(Vector3.right, Vector3.zero, Vector3.zero)
                };
            }
        }

        public Vector3 GetPosition(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);

            var cl = ratio * Length;

            foreach (var pair in PairList)
            {
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    return pair.GetPoisition(cl / pair.Length);
            }
            return option.mode == LineOption.Mode.Loop ? Points.First().position : Points.Last().position;
        }

        public Vector3 GetDirection(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            var cl = ratio * Length;
            Vector3 dir = Vector3.zero;
            foreach (var pair in PairList)
            {
                dir = pair.GetDirection(cl / pair.Length);
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    break;
            }
            return dir;
        }


        public float AllLength {
            get
            {
                return Pair.Sum(p => CurveLength.Auto(p[0], p[1]));
            }
        }
        public float Length
        {
            get
            {
                return PairList.Sum(p => p.Length);
            }
        }

        public Vector2 UVRotate(Vector2 uv)
        {
            uv -= Vector2.one / 2f;
            uv = Quaternion.Euler(0,0,option.uvAngle)*uv;
            uv += Vector2.one / 2f;
            return uv;
        }
    }
}