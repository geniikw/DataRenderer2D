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
        [Header("if you want to animate position of node, set false")]
        public bool splineMode;
        [Header("it's valid when SplineMode is false")]
        public LinePair pair;
        [Header("it's valid when SplineMode is true")]
        public List<Point> points;
        
        public IList<Point> Points
        {
            get
            {
                if (splineMode)
                    return points;
                else
                    return new Point[] { pair.n0, pair.n1 };
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
                    pair = new LinePair(Point.Zero, new Point(Vector3.right, Vector3.zero, Vector3.zero), 0, 1, 0, 1)
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

    }
}