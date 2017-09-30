using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.UIMeshLab
{
    [Serializable]
    public struct Node
    {
        public Vector3 position;
        public Vector3 previousCurvePosition;
        public Vector3 nextCurvePosition;
        public float width;
        public float angle;
        public int divieCount;
        public bool loop;
    }
}