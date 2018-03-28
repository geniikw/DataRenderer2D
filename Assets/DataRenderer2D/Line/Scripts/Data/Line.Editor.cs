using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.DataRenderer2D
{
    public partial struct Spline
    {
        public event Action EditCallBack;
        public MonoBehaviour owner;
        
        public void Initialize() {
            this = Default;
        }

        public void Push(Point p)
        {
            if (mode == LineMode.BezierMode)
                throw new Exception("can't add");

            points.Add(p);

            if (EditCallBack != null)
                EditCallBack();
        }

        public void Push(Vector3 position, Vector3 nextOffset, Vector3 prevOffset, float width)
        {
            Push(new Point(position, nextOffset, prevOffset, width));
        }

        public void EditPoint(int idx, Point p)
        {
            if(mode == LineMode.BezierMode &&( idx <0 || idx > 2))
            {
                throw new Exception("can't edit");
            }
            if (points.Count <= idx || idx < 0)
            {
                throw new Exception("can't edit");
            }
            
            if (mode == LineMode.BezierMode)
            {
                if (idx == 0)
                    pair.n0 = p;
                else
                    pair.n1 = p;
            }
            else
            {
                points[idx] = p;
            }

            if (EditCallBack != null)
                EditCallBack();
        }

        public void EditPoint(int idx, Vector3 pos, Vector3 nOffset, Vector3 pOffset, float width)
        {
            EditPoint(idx, new Point(pos, nOffset, pOffset, width));
        }

        /// <summary>
        /// remove last point
        /// </summary>
        /// <returns></returns>
        public Point Pop()
        {
            if (mode == LineMode.BezierMode)
                throw new Exception("can't remove");

            var last = points[points.Count - 1];
            points.RemoveAt(points.Count - 1);

            if (EditCallBack != null)
                EditCallBack();

            return last;
        }
        
    }
}