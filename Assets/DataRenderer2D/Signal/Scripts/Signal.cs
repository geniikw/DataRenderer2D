using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Signal
{
    public enum ESignalType
    {
        Sin,
        Square,
        Triangle,
        Sawtooth
    }


    [Serializable]
    public struct SignalData
    {
        public float divide;
        public float amplify;
        public float frequncy;
        public float timeFactor;

        public ESignalType type;

        public AnimationCurve ampUpCurve;
        public AnimationCurve ampDownCurve;
        //public AnimationCurve ampRightCurve;
        //public AnimationCurve ampLeftCurve;
        [SerializeField]
        Gradient color;
        public Gradient Color
        {
            get
            {
                return color ?? (color = new Gradient());
            }
        }


        public bool up;
        public bool down;
        //public bool right;
        //public bool left;

        public static SignalData Default
        {
            get
            {
                return new SignalData()
                {
                    divide = 1f,
                    color = new Gradient(),
                    ampUpCurve = AnimationCurve.Constant(0, 1, 1),
                    ampDownCurve = AnimationCurve.Constant(0, 1, 1),
                    amplify = 1f,
                    frequncy = 1f,
                    timeFactor = 100
                };
           
            }
        }
    }
}