using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.DataRenderer2D
{
    public partial struct Spline
    {
        public void Initialize() {
            this = Default;
        }

        /// <summary>
        /// 중간에 p.position이 worldposition에서  localposition으로 바꿈.
        /// </summary>
        /// <param name="p"></param>
        public void Push(Point p)
        {
            if (mode == LineMode.BezierMode)
                throw new Exception("can't add");

            p.position = owner.transform.InverseTransformPoint(p.position);

            points.Add(p);

            if (EditCallBack != null)
                EditCallBack();
        }

        public void Push()
        {
            Push(Point.Zero);
        }


        public void Push(Vector3 worldPosition, Vector3 nextOffset, Vector3 prevOffset, float width)
        {
            Push(new Point(worldPosition, nextOffset, prevOffset, width));
        }

        public void EditPoint(int idx, Point p)
        {
            if(mode == LineMode.BezierMode &&( idx <0 || idx > 2))
            {
                throw new Exception("can't edit");
            }
            if (points.Count <= idx || idx < 0)
            {
                throw new Exception("can't edit" + points.Count + " " + idx);
            }

            p.position = owner.transform.InverseTransformPoint(p.position);
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

        /// <summary>
        /// edit last point.
        /// </summary>
        /// <param name="worldPos"></param>
        public void EditPoint(Vector3 worldPos)
        {
            EditPoint(points.Count - 1, new Point(worldPos, Vector3.zero, Vector3.zero));
        }

        public void EditPoint(int idx, Vector3 worldPos, Vector3 nOffset, Vector3 pOffset, float width)
        {
            EditPoint(idx, new Point(worldPos, nOffset, pOffset, width));
        }

        public void EditPoint(int idx, Vector3 worldPos, float width)
        {
            EditPoint(idx,worldPos, Vector3.zero, Vector3.zero, width);
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
        
        public int Count
        {
            get
            {
                if (mode == LineMode.BezierMode)
                    return 2;
                return points.Count;
            }
        }
        public void Clear()
        {
            points.Clear();
            if (EditCallBack != null)
                EditCallBack();
        }

    }
}