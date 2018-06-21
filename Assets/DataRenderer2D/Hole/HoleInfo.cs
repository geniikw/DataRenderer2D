using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace geniikw.DataRenderer2D.Hole
{

    [Serializable]
    public struct HoleInfo
    {
        [SerializeField]
        [Range(3, 30)]
        private int inner;



        [SerializeField]
        [Range(-0.5f, 0.5f)]
        private float offsetY;

        [SerializeField]
        [Range(-0.5f, 0.5f)]
        private float offsetX;
        
        [SerializeField]
        [Range(0, 1)]
        private float sizeX;

        [SerializeField]
        [Range(0, 1)]
        private float sizeY;

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

        public float OffsetY
        {
            get
            {
                return offsetY;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                offsetY = value;
            }
        }

        public float OffsetX
        {
            get
            {
                return offsetX;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                offsetX = value;
            }
        }

        public float SizeX
        {
            get
            {
                return sizeX;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                sizeX = value;
            }
        }

        public float SizeY
        {
            get
            {
                return sizeY;
            }

            set
            {
                if (editCallback != null)
                {
                    editCallback();
                }
                sizeY = value;
            }
        }
    }
}