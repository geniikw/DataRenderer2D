using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{

    /// <summary>
    /// data container of bezierLine.
    /// </summary>

    [Serializable]
    public partial class Line
    {
        
        public enum Mode
        {
            Noraml=0,
            Loop=1,
            RoundEdge=2
        }

        public List<Point> points = new List<Point>();
        [Range(0, 1)]
        public float startRatio = 0f;
        [Range(0, 1)]
        public float endRatio = 1f;
        public Mode mode = Mode.Noraml;
        [Range(1,100)]
        public float divideLength = 1f;
        [Range(5, 180)]
        public float divideAngle = 10f;

        public float uvAngle = 0f;
        public Gradient color = new Gradient();

        public Vector3 normalVector = Vector3.forward;

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
            return mode==Mode.Loop ?points.First().position : points.Last().position;
        }
        
        public Vector3 GetDirection(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            var cl = ratio * Length;

            foreach (var pair in PairList)
            {
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    return pair.GetDirection(cl / pair.Length);
            }
            return (points[points.Count-2].position-points.Last().position).normalized;
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