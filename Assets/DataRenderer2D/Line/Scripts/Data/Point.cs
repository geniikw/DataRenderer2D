﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    /// define each point
    /// </summary>
    [Serializable]
    public struct Point
    {
        public Vector3 position;
        public Vector3 previousControlOffset;
        public Vector3 nextControlOffset;
        [Range(0,100)]
        public float width;
        [Range(-90, 90)]
        public float forwardAngle;
        [Range(-90, 90)]
        public float backAngle;
        //todo : move normalVector from LineOption to Point.
        //public float normal;

        public Point(Vector3 pos, Vector3 next, Vector3 prev, float width = 2,float angle = 0, float endAngle = 0)
        {
            position = pos;
            previousControlOffset = prev;
            nextControlOffset = next;
            this.forwardAngle = angle;
            this.width = width;
            this.backAngle = endAngle;
        }
        
        public Vector3 PreviousControlPoisition
        {
            get
            {
                return previousControlOffset + position;
            }
        }
        public Vector3 NextControlPosition
        {
            get
            {
                return nextControlOffset + position;
            }
        }

        public static Point Zero
        {
            get
            {
                return new Point(Vector3.zero, Vector3.zero, Vector3.zero);
            }
        }
    }
}