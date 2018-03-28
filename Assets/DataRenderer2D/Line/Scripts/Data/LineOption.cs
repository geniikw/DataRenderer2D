using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.DataRenderer2D
{
    [Serializable]
    public struct LineOption
    {

        public enum Mode
        {
            Noraml = 0,
            Loop = 1,
            RoundEdge = 2
        }

        public enum LineJointOption
        {
            round,
            intersect
        }

        [Range(0, 1)]
        public float startRatio;
        [Range(0, 1)]
        public float endRatio;
        public Mode mode;
        [Range(0.1f, 100f)]
        public float divideLength;
        [Range(5, 180)]
        public float divideAngle;

        public Gradient color;//class reference type;

        public LineJointOption jointOption;

        public float DivideAngle
        {
            get { return Mathf.Clamp(divideAngle, 5, 180); }
        }
        public float DivideLength
        {
            get { return Mathf.Clamp(divideLength, 0.1f, 100); }
        }

        public static LineOption Default
        {
            get
            {
                return new LineOption()
                {
                    startRatio = 0f,
                    endRatio = 1f,
                    mode = Mode.Noraml,
                    divideLength = 1f,
                    divideAngle = 10f,
                    color = new Gradient()
                };
            }
        }

    }
}