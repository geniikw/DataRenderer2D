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
            Noraml=0,
            Loop=1,
            RoundEdge=2
        }

        public List<Point> points;
        [Range(0, 1)]
        public float startRatio;
        [Range(0, 1)]
        public float endRatio;
        public Mode mode;
        [Range(1, 100)]
        public float divideLength;
        [Range(5, 180)]
        public float divideAngle;

        public float uvAngle;
        public Gradient color;

        public Vector3 normalVector;

        public Spline(int number)
        {
            points = new List<Point>();
            startRatio = 0f;
            endRatio = 1f;
            mode = Mode.Noraml;
            divideLength = 1f;
            divideAngle = 10f;
            uvAngle = 0f;
            color = new Gradient();
            normalVector = Vector3.back;

        }

        public Vector3 GetPosition(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);

            var cl = ratio * Length;
            
            foreach(var pair in PairList)
            {
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    return pair.GetPoisition(cl / pair.Length);
            }
            return mode==Mode.Loop ?points[0].position : points.Last().position;
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
   
                
        public float AllLength => Pair.Sum(p => CurveLength.Auto(p[0], p[1]));
        public float Length => PairList.Sum(p => p.Length);

        public Vector2 UVRotate(Vector2 uv)
        {
            uv -= Vector2.one / 2f;
            uv = Quaternion.Euler(0,0,uvAngle)*uv;
            uv += Vector2.one / 2f;
            return uv;
        }
    }
}