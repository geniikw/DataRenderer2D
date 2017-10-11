using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.UIMeshLab
{
    /// <summary>
    /// define each point
    /// </summary>
    [Serializable]
    public class Point
    {
        public Vector3 position;
        public Vector3 previousControlOffset;
        public Vector3 nextControlOffset;
        [Range(0,100)]
        public float width;
        //[Range(0,180)]
        //public float angle;

        public Point()
        {
            position = Vector3.zero;
            previousControlOffset = Vector3.zero;
            nextControlOffset = Vector3.zero;

            width = 2f;
//            angle = 0f;
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
    }
}