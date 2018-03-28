using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Polygon
{
    public interface IPolygon
    {
        PolygonData Polygon{ get; }
    }

    [Serializable]
    public struct PolygonData
    {
        public PolygonType type;
        [Range(0, 1.414f)]
        public float sinCft;//coefficient
        [Range(0, 1.414f)]
        public float cosCft;
        [Range(0, 1)]
        public float startAngle;
        [Range(0, 1)]
        public float endAngle;
        public int count;
        [Range(0, 1)]
        public float innerRatio;
        public Color centerColor;
        
        public Gradient outerColor;
        
        public AnimationCurve curve;
    }

    public enum PolygonType
    {
        ZigZag,
        Hole,
        HoleCurve,
        HoleHalf,
        HoleCenterColor,
        HoleHalfCenterColor,
        
    }
}