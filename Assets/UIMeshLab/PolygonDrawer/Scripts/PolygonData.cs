using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab.Polygon
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
        public Gradient color;
        [Range(0, 1)]
        public float innerRatio;
    }

    public enum PolygonType
    {
        Simple,
        CenterVertex,
        Hole=5,
        HoleHalf,
    }
}