using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Signal
{
    public enum ESignalType
    {
        Sin
    }
    
    [Serializable]
    public struct SignalData
    {
        [Serializable]
        public struct SignalOneSet
        {
            public bool use;
            public float amplify;
            public float frequncy;
            public float timeFactor;
            public AnimationCurve AmpCurve;
            public ESignalType type;

            public float Output(float x, float t)
            {  
                return amplify * TypeSwitch((x + t* timeFactor) * frequncy);
            }

            private float TypeSwitch(float x)
            {  
                switch (type)
                {
                    case ESignalType.Sin:
                        return Mathf.Sin(x / Mathf.PI * 2);
                    default:
                        return 0f;
                }
            }

            public static SignalOneSet Default
            {
                get
                {
                    return new SignalOneSet()
                    {
                        use = true,
                        AmpCurve = AnimationCurve.Linear(0, 0, 1, 1),
                        amplify = 10,
                        frequncy = 10,
                        timeFactor = 1,
                        type = ESignalType.Sin
                    };
                }
            }
        }

        public float t;
        public SignalOneSet up;
        public SignalOneSet down;

        public float divide;

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
                
        //public bool right;
        //public bool left;

        public static SignalData Default
        {
            get
            {
                return new SignalData()
                {
                    color = new Gradient(),
                    up = SignalOneSet.Default,
                    down = SignalOneSet.Default,
                    divide = 5f,
                    t = 0f
                };
           
            }
        }
    }
}