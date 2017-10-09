using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    /// Line 
    ///     List<Node> points 
    ///          Vector3 position;
    ///          Vector3 previousCurvePosition;
    ///          Vector3 nextCurvePosition;
    ///          Color color;
    ///          float width;
    ///          float angle;
    ///          int nextDivieCount;
    ///     bool loop;
    ///     float startRatio
    ///     float endRatio

    [Serializable]
    public partial class Line
    {
        public List<Node> points = new List<Node>();
        [Range(0, 1)]
        public float startRatio = 0f;
        [Range(0, 1)]
        public float endRatio = 1f;
        public bool loop = false;
        [Range(1,100)]
        public float divideLength = 1f;

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
            return points.Last().position;
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
        public float uvAngle = 0f;

        public Vector2 UVRotate(Vector2 uv)
        {
            uv -= Vector2.one / 2f;
            uv = Quaternion.Euler(0,0,uvAngle)*uv;
            uv += Vector2.one / 2f;
            return uv;
        }
    }
}