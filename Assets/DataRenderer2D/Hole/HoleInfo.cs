using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.DataRenderer2D.Hole
{

    [Serializable]
    public struct HoleInfo
    {
        [SerializeField][Range(3, 30)]
        private int inner;
        [SerializeField]
        private Vector2 offset;
        [SerializeField]
        private Vector2 size;
        [SerializeField]
        private float angle;
        [SerializeField]
        private Color color;

        public Action editCallback;
            
        public int Inner
        {
            get
            {
                return inner;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                inner = value;
            }
        }

        public Vector2 Offset
        {
            get
            {
                return offset;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                offset = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                size = value;
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                angle = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                color = value;
            }
        }
    }
}